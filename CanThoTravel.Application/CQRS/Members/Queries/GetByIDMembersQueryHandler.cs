using CanThoTravel.Application.DTOs.Member;
using CanThoTravel.Application.Repository;
using MediatR;

namespace CanThoTravel.Application.CQRS.Members.Queries
{
    public class GetByIDMembersQueryHandler : IRequestHandler<GetByIDMembersQuery, MemberResponseDTO?>
    {
        private readonly IMemberRepository _memberRepository;

        public GetByIDMembersQueryHandler(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<MemberResponseDTO?> Handle(GetByIDMembersQuery request, CancellationToken cancellationToken)
        {
            var entity = await _memberRepository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                return null;
            }

            return new MemberResponseDTO
            {
                Id = entity.Id,
                FullName = entity.Name,
            };
        }
    }
}