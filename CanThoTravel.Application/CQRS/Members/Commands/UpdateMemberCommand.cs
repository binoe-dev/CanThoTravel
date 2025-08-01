using CanThoTravel.Application.DTOs.Member;
using MediatR;

namespace CanThoTravel.Application.CQRS.Members.Commands
{
    public record UpdateMemberCommand(UpdateMemberRequestDTO Dto) : IRequest;
}