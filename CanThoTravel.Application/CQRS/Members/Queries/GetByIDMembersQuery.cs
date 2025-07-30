using CanThoTravel.Application.DTOs.Member;
using MediatR;

namespace CanThoTravel.Application.CQRS.Members.Queries
{
    public record GetByIDMembersQuery(string Id) : IRequest<MemberResponseDTO?>;
}