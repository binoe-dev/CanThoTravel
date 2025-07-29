using CanThoTravel.Application.Repository;
using CanThoTravel.Domain.Entities.Member;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CanThoTravel.Application.CQRS.Members.Queries
{
    public class GetAllMembersQueryHandler : IRequestHandler<GetAllMembersQuery, List<MemberEntity>>
    {
        private readonly IMemberRepository _memberRepository;

        public GetAllMembersQueryHandler(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<List<MemberEntity>> Handle(GetAllMembersQuery request, CancellationToken cancellationToken)
        {
            // Uses the existing repository logic, which currently returns a mock list.
            return await _memberRepository.GetAll();
        }
    }
}