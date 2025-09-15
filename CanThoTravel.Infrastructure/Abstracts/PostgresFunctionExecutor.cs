using CanThoTravel.Application.IRepositories.PostgreSQL;
using Npgsql;
using System.Data;
using System.Reflection;

namespace CanThoTravel.Infrastructure.Abstracts
{
    public abstract class PostgresFunctionBase<TEntity> where TEntity : class
    {
        private readonly NpgsqlConnection _connection;
        private readonly ITransactionManager _transactionManager;

        public PostgresFunctionBase(NpgsqlConnection npgsqlConnection, ITransactionManager transactionManager) {
            _connection = npgsqlConnection;
            _transactionManager = transactionManager;
        }

        #region Query helper methods

        //------ Insert, update, delete without return
        public async Task ExecuteVoidFunctionAsync(string functionName, Dictionary<string, object>? parameters = null, int? commandTimeout = null)
        {
            await ExecuteInTransactionAsync(async () =>
            {
                var paramStr = parameters?.Select(p => $"{p.Key} := @{p.Key}").ToList() ?? [];
                var sql = $"PERFORM {functionName}({string.Join(", ", paramStr)});";

                await using var cmd = CreateCommand(sql, parameters);
                var tx = _transactionManager.GetCurrentTransaction();
                cmd.Transaction = tx;
                if (commandTimeout.HasValue) cmd.CommandTimeout = commandTimeout.Value;

                return await cmd.ExecuteNonQueryAsync();
            });
        }

        //------ Insert, update, delete with return 
        public async Task<int> ExecuteNonQueryFunctionAsync(string functionName, Dictionary<string, object>? parameters = null, int? commandTimeout = null)
        {
            return await ExecuteInTransactionAsync(async () =>
            {
                var paramStr = parameters?.Select(p => $"{p.Key} := @{p.Key}").ToList() ?? [];
                var sql = $"SELECT {functionName}({string.Join(", ", paramStr)});";

                await using var cmd = CreateCommand(sql, parameters);
                var tx = _transactionManager.GetCurrentTransaction();
                cmd.Transaction = tx;
                if (commandTimeout.HasValue) cmd.CommandTimeout = commandTimeout.Value;

                var result = await cmd.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            });
        }

        //------- Query with cursor
        protected async Task<IEnumerable<TResult>> ExecuteFunctionWithCursorAsync<TResult>(string functionName, Dictionary<string, object>? parameters = null, string cursorName = "v_out", int? commandTimeout = null)
        {
            return await ExecuteInTransactionAsync(async () =>
            {
                try
                {
                    var currentTransaction = _transactionManager.GetCurrentTransaction();
                    if (currentTransaction == null)
                        throw new InvalidOperationException("A transaction is required for cursor operations.");

                    // Call the function
                    var paramList = parameters?.Select(p => $"{p.Key} := @{p.Key}").ToList() ?? [];
                    paramList.Insert(0, $"'{cursorName}'");
                    var paramString = string.Join(", ", paramList);

                    var sql = $"SELECT {functionName}({paramString});";
                    await using (var cmd = CreateCommand(sql, parameters))
                    {
                        cmd.Transaction = currentTransaction;
                        if (commandTimeout.HasValue) cmd.CommandTimeout = commandTimeout.Value;

                        await cmd.ExecuteNonQueryAsync();
                    }

                    // Fetch all records from cursor
                    var results = await FetchAllFromCursorAsync<TResult>(cursorName);

                    // Close the cursor
                    await using (var cmd = new NpgsqlCommand($"CLOSE {cursorName}", _connection, currentTransaction)) { await cmd.ExecuteNonQueryAsync(); }

                    return results;
                }
                catch (Exception ex)
                {
                    // Attempt to close the cursor in case of error
                    try
                    {
                        await using var cmd = new NpgsqlCommand($"CLOSE {cursorName}", _connection, _transactionManager.GetCurrentTransaction());
                        await cmd.ExecuteNonQueryAsync();
                    }
                    catch
                    {
                        // Ignore errors when closing cursor
                    }

                    throw;
                }
            });
        }

        #endregion

        #region General

        //---- Open connection if not already open
        private async Task EnsureConnectionOpenAsync()
        {
            if (_connection.State != ConnectionState.Open)
            {
                try { await _connection.OpenAsync(); }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Failed to open database connection.", ex);
                }
            }
        }

        private NpgsqlCommand CreateCommand(string sql, Dictionary<string, object>? parameters = null)
        {
            var cmd = new NpgsqlCommand(sql, _connection);
            if (parameters != null)
            {
                foreach (var p in parameters)
                    cmd.Parameters.AddWithValue(p.Key, p.Value ?? DBNull.Value);
            }

            return cmd;
        }

        protected async Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> action)
        {
            if (_transactionManager == null)
                throw new InvalidOperationException("Transaction manager not initialized.");

            var tx = _transactionManager.GetCurrentTransaction();
            var isNewTx = tx == null;

            if (isNewTx)
            {
                tx = await _transactionManager.BeginTransactionAsync();
                if (tx == null) throw new InvalidOperationException("Failed to begin transaction.");
            }

            try
            {
                var result = await action();

                if (isNewTx)
                    await _transactionManager.CommitAsync(tx);

                return result;
            }
            catch (Exception ex)
            {
                if (isNewTx)
                    await _transactionManager.RollbackAsync(tx);

                throw;
            }
            finally
            {
                if (isNewTx)
                    await tx.DisposeAsync();
            }
        }

        private async Task<List<T>> FetchAllFromCursorAsync<T>(string cursorName)
        {
            var currentTransaction = _transactionManager.GetCurrentTransaction();
            if (currentTransaction == null)
                throw new InvalidOperationException("A transaction is required for cursor operations.");

            var results = new List<T>();
            await using var cmd = new NpgsqlCommand($"FETCH ALL FROM {cursorName}", _connection, currentTransaction);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync()) { results.Add(MapToEntity<T>(reader)); }

            return results;
        }

        private T MapToEntity<T>(NpgsqlDataReader reader)
        {
            var entity = Activator.CreateInstance<T>();
            var properties = typeof(T).GetProperties();
            var columnSchema = Enumerable.Range(0, reader.FieldCount)
                .Select(i => new { Name = reader.GetName(i), Index = i })
                .ToDictionary(x => x.Name.ToLower(), x => x.Index);

            foreach (var prop in properties)
            {
                var columnName = prop.Name;
                if (!columnSchema.ContainsKey(columnName.ToLower()))
                    continue;

                var value = reader.IsDBNull(columnSchema[columnName.ToLower()]) ? null : reader.GetValue(columnSchema[columnName.ToLower()]);

                if (value != null) { SetPropertyValue(entity, prop, value); }
            }

            return entity;
        }

        private void SetPropertyValue<T>(T entity, PropertyInfo prop, object value)
        {
            try
            {
                if (prop.PropertyType == typeof(int) && value is long longValue) { prop.SetValue(entity, (int)longValue); }
                else if (prop.PropertyType.IsEnum)
                {
                    if (value is string stringValue) { prop.SetValue(entity, Enum.Parse(prop.PropertyType, stringValue)); }
                    else { prop.SetValue(entity, Enum.ToObject(prop.PropertyType, value)); }
                }
                else if (prop.PropertyType == typeof(Guid) && value is string guidString) { prop.SetValue(entity, Guid.Parse(guidString)); }
                else if (prop.PropertyType == typeof(TimeSpan) && value is string timeString) { prop.SetValue(entity, TimeSpan.Parse(timeString)); }
                else if (prop.PropertyType == typeof(bool) && value is short shortValue) { prop.SetValue(entity, shortValue != 0); }
                else
                {
                    var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    var convertedValue = Convert.ChangeType(value, targetType);
                    prop.SetValue(entity, convertedValue);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error mapping property {prop.Name} with value {value}", ex);
            }
        }

        #endregion
    }
}
