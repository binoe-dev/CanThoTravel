using CleanArc.Application.Repository.PostgreSQL;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CleanArc.Infrastructure.Abstracts
{
    public abstract class PostgresFunctionExecutor<TEntity> where TEntity : class
    {
        private readonly NpgsqlConnection _connection;
        private readonly ILogger<PostgresFunctionExecutor<TEntity>> _logger;
        private readonly ITransactionManager _transactionManager;

        public PostgresFunctionExecutor(NpgsqlConnection npgsqlConnection, ILogger<PostgresFunctionExecutor<TEntity>> logger, ITransactionManager transactionManager) {
            _connection = npgsqlConnection;
            _logger = logger;
            _transactionManager = transactionManager;
        }

        #region Method

        //------ Insert, update, delete
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

                return await cmd.ExecuteNonQueryAsync();
            });
        }

        //------- Query list
        public async Task<IEnumerable<T>> ExecuteReaderFunctionAsync<T>(string functionName, Func<IDataReader, T> mapper, Dictionary<string, object>? parameters = null, int? commandTimeout = null)
        {
            return await ExecuteInTransactionAsync(async () =>
            {
                var results = new List<T>();
                var paramStr = parameters?.Select(p => $"{p.Key} := @{p.Key}").ToList() ?? [];
                var sql = $"SELECT * FROM {functionName}({string.Join(", ", paramStr)});";

                await using var cmd = CreateCommand(sql, parameters);
                var tx = _transactionManager.GetCurrentTransaction();
                cmd.Transaction = tx;
                if (commandTimeout.HasValue) cmd.CommandTimeout = commandTimeout.Value;

                await using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    results.Add(mapper(reader));
                }

                return results;
            });
        }

        #endregion

        #region General
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

                _logger.LogError(ex, "Transaction failed.");
                throw;
            }
            finally
            {
                if (isNewTx)
                    await tx.DisposeAsync();
            }
        }
        #endregion
    }
}
