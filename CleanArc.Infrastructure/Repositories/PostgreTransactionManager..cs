using CleanArc.Application.Repository.PostgreSQL;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Repositories
{
    public class PostgreTransactionManager : ITransactionManager
    {
        private readonly NpgsqlConnection _connection;
        private NpgsqlTransaction? _transaction;
        private readonly IConfiguration _configuation;
        private CancellationTokenSource? _timeoutCts;

        public PostgreTransactionManager(NpgsqlConnection npgsqlConnection, IConfiguration configuration)
        {
            _connection = npgsqlConnection;
            _configuation = configuration;
        }


        #region Method
        public async Task<NpgsqlTransaction> BeginTransactionAsync()
        {
            if (_transaction != null)
            {
                return _transaction;
            }
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    await _connection.OpenAsync();
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Failed to open database connection.", ex);
                }
            }
            _timeoutCts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
            _transaction = await _connection.BeginTransactionAsync();
            return _transaction;
        }
        #endregion 




        public async Task CommitAsync(NpgsqlTransaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("Transaction cannot be null");
            }
            try 
            {
                await transaction.CommitAsync(); 
            }
            finally
            {
                _transaction = null;
                _timeoutCts?.Dispose();
                _timeoutCts = null;
            }
        }

        public NpgsqlTransaction? GetCurrentTransaction()
        {
            return _transaction;
        }

        public async Task RollbackAsync(NpgsqlTransaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("Transaction cannot be null");
            }
            try
            {
                await transaction.RollbackAsync();
            }
            finally
            {
                _transaction = null;
                _timeoutCts?.Dispose();
                _timeoutCts = null;
            }
        }
    }
}
