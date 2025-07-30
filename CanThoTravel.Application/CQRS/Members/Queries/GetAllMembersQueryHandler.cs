using CanThoTravel.Application.DTOs.Member;
using CanThoTravel.Application.Repository;
using CanThoTravel.Domain.Entities.Member;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CanThoTravel.Application.CQRS.Members.Queries
{
    public class GetAllMembersQueryHandler : IRequestHandler<GetAllMembersQuery, List<MemberResponseDTO>>
    {
        private readonly IMemberRepository _memberRepository;

        public GetAllMembersQueryHandler(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<List<MemberResponseDTO>> Handle(GetAllMembersQuery request, CancellationToken cancellationToken)
        {
            // Uses the existing repository logic, which currently returns a mock list.
            var lstEntities = await _memberRepository.GetAll();
            if (lstEntities == null || lstEntities.Count == 0) return new List<MemberResponseDTO>();

            return lstEntities.Select(member => new MemberResponseDTO
            {
                Id = member.Id,
                FullName = member.Name,
            }).ToList();
        }
    }
}