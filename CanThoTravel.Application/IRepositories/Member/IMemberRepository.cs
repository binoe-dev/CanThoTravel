using CanThoTravel.Domain.Entities.Member;

namespace CanThoTravel.Application.IRepositories.Member
{
    public interface IMemberRepository
    {
        Task<List<MemberEntity>> GetAllAsync();
        Task<MemberEntity?> GetByIdAsync(int id);
        Task<MemberEntity?> GetByEmailAsync(string email);
        Task<int> AddAsync(MemberEntity member);
        Task UpdateAsync(MemberEntity member);
        Task DeleteAsync(int id);
    }
}
