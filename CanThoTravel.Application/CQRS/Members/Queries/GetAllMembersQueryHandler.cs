using AutoMapper;
using CanThoTravel.Application.DTOs.Member;
using CanThoTravel.Application.IRepositories.Member;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CanThoTravel.Application.CQRS.Members.Queries
{
    public class GetAllMembersQueryHandler : IRequestHandler<GetAllMembersQuery, List<MemberResponseDTO>>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public GetAllMembersQueryHandler(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<List<MemberResponseDTO>> Handle(GetAllMembersQuery request, CancellationToken cancellationToken)
        {
            // Uses the existing repository logic, which currently returns a mock list.
            var lstEntities = await _memberRepository.GetAllAsync();
            if (lstEntities == null || lstEntities.Count == 0) return new List<MemberResponseDTO>();

            return _mapper.Map<List<MemberResponseDTO>>(lstEntities);
        }
    }
}