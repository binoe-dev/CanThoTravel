using CanThoTravel.Application.CQRS.Members.Queries;
using CanThoTravel.Application.Repository;
using CanThoTravel.Domain.Entities.Member;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanThoTravel.Application.Service.Member
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _IMemberRepository;
        private readonly IMediator _IMediator;

        //public MemberService(IMemberRepository IMemberRepository)
        //{
        //    _IMemberRepository = IMemberRepository;
        //}

        //public async Task<List<MemberEntity>> Get()
        //{
        //    return await _IMemberRepository.Get();
        //}

        public MemberService(IMediator mediator)
        {
            _IMediator = mediator;
        }

        public async Task<List<MemberEntity>> Get()
        {
            var query = new GetAllMembersQuery();
            var result = await _IMediator.Send(query);
            return result;
        }
    }
}
