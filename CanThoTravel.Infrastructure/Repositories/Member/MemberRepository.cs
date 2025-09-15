using CanThoTravel.Application.IRepositories.Member;
using CanThoTravel.Application.IRepositories.PostgreSQL;
using CanThoTravel.Domain.Entities.Member;
using CanThoTravel.Infrastructure.Abstracts;
using Npgsql;

namespace CanThoTravel.Infrastructure.Repositories.Member
{
    public class MemberRepository : PostgresFunctionBase<MemberEntity>, IMemberRepository
    {
        public MemberRepository(NpgsqlConnection npgsqlConnection, ITransactionManager transactionManager) : base(npgsqlConnection, transactionManager)
        {
        }

        public async Task<List<MemberEntity>> GetAllAsync()
        {
            var lstParams = new Dictionary<string, object>();
            var result = await ExecuteFunctionWithCursorAsync<MemberEntity>("masterdata.get_all_members", lstParams);
            return result.ToList();
        }

        public async Task<MemberEntity?> GetByIdAsync(int id)
        {
            var lstParams = new Dictionary<string, object>();
            lstParams.Add("p_id", id);

            var result = await ExecuteFunctionWithCursorAsync<MemberEntity>("masterdata.get_members_by_id", lstParams);
            return result.FirstOrDefault();
        }

        public async Task<MemberEntity?> GetByEmailAsync(string email)
        {
            var lstParams = new Dictionary<string, object>
            {
                { "p_email", email }
            };
            var result = await ExecuteFunctionWithCursorAsync<MemberEntity>("masterdata.get_members_by_email", lstParams);
            return result.FirstOrDefault();
        }

        public async Task<int> AddAsync(MemberEntity member)
        {
            var lstParams = new Dictionary<string, object>
            {
                { "p_name", member.Name },
                { "p_email", member.Email },
                { "p_password_hash", member.PasswordHash },
                { "p_type", member.Type },
                { "p_address", member.Address }
            };

            return await ExecuteNonQueryFunctionAsync("masterdata.add_member", lstParams);
        }

        public async Task UpdateAsync(MemberEntity member)
        {
            var lstParams = new Dictionary<string, object>
            {
                { "p_id", member.Id },
                { "p_name", member.Name },
                { "p_type", member.Type },
                { "p_address", member.Address }
            };

            await ExecuteVoidFunctionAsync("masterdata.update_member", lstParams);
        }
        public async Task DeleteAsync(int id)
        {
            var lstParams = new Dictionary<string, object>
            {
                { "p_id", id }
            };

            await ExecuteVoidFunctionAsync("masterdata.delete_member", lstParams);
        }
    }
}
