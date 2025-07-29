using CanThoTravel.Domain.DTO;
using CanThoTravel.Application.Repository;
using CanThoTravel.Domain.Entities.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanThoTravel.Infrastructure.UnitOfWork.Member
{
    public class MemberReadUnitOfWork
    {
        private readonly IMemberRepository _memberRepository;
        public MemberReadUnitOfWork(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }
        public async Task<List<MemberEntity>> Get(GetMemberDTO dTO)
        {
            try
            {
                if (dTO == null)
                {
                    throw new ArgumentNullException(nameof(dTO), "DTO cannot be null");
                }
                var members = await _memberRepository.Get();
                if (members == null || !members.Any())
                {
                    return new List<MemberEntity>();
                }

                return members;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
