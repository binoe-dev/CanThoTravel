using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Repository.PostgreSQL
{
    public interface ITransactionManager
    {
        NpgsqlTransaction? GetCurrentTransaction();
        Task<NpgsqlTransaction> BeginTransactionAsync();
        Task CommitAsync(NpgsqlTransaction transaction);
        Task RollbackAsync(NpgsqlTransaction transaction);
    }
}
