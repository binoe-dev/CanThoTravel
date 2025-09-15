using Npgsql;

namespace CanThoTravel.Application.IRepositories.PostgreSQL
{
    public interface ITransactionManager
    {
        NpgsqlTransaction? GetCurrentTransaction();
        Task<NpgsqlTransaction> BeginTransactionAsync();
        Task CommitAsync(NpgsqlTransaction transaction);
        Task RollbackAsync(NpgsqlTransaction transaction);
    }
}
