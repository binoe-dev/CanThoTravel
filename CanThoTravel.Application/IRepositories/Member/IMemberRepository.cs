using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CanThoTravel.Domain.Entities.Member;

namespace CanThoTravel.Application.Repository
{
    public interface IMemberRepository
    {
        Task<List<MemberEntity>> GetAllAsync();
        Task<MemberEntity?> GetByIdAsync(int id);
        Task<MemberEntity?> GetByEmailAsync(string email);
        Task<int> AddAsync(MemberEntity member);
        Task UpdateAsync(MemberEntity member);
    }
}
