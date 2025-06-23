using CleanArc.Application.Repository.Member;
using CleanArc.Domain.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Service.Member
{
    public class MemberService : IMemberService
    {
        public readonly IMemberRepository _IMemberRepository;
        public MemberService(IMemberRepository IMemberRepository)
        {
            _IMemberRepository = IMemberRepository;
        }

        public async Task<List<MemberDomain>> Get()
        {
            return await _IMemberRepository.Get();
        }
    }
}
