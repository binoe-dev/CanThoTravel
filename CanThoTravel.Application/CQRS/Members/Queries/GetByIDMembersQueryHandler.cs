using AutoMapper;
using CanThoTravel.Application.DTOs.Member;
using CanThoTravel.Application.IRepositories.Member;
using MediatR;

namespace CanThoTravel.Application.CQRS.Members.Queries
{
    public class GetByIDMembersQueryHandler : IRequestHandler<GetByIDMembersQuery, MemberResponseDTO?>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public GetByIDMembersQueryHandler(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<MemberResponseDTO?> Handle(GetByIDMembersQuery request, CancellationToken cancellationToken)
        {
            var entity = await _memberRepository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                return null;
            }

            return _mapper.Map<MemberResponseDTO>(entity);
        }
    }
}