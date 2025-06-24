using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArc.Application.Repository;
using CleanArc.Domain.Entities.Member;

namespace CleanArc.Application.Service.Member
{
    public class MemberService : IMemberService
    {
        public readonly IMemberRepository _IMemberRepository;
        public MemberService(IMemberRepository IMemberRepository)
        {
            _IMemberRepository = IMemberRepository;
        }

        public async Task<List<MemberEntity>> Get()
        {
            return await _IMemberRepository.Get();
        }
    }
}
