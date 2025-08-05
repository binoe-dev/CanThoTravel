using CanThoTravel.Application.DTOs.Member;
using MediatR;

namespace CanThoTravel.Application.CQRS.Members.Commands
{
    public record AddMemberCommand(AddMemberRequestDTO Dto) : IRequest<int>;
}