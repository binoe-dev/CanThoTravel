using CanThoTravel.Application.DTOs.Member;
using MediatR;

namespace CanThoTravel.Application.CQRS.Members.Queries
{
    public record GetByIDMembersQuery(int Id) : IRequest<MemberResponseDTO?>;
}