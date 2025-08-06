using CanThoTravel.Application.DTOs.Authentication;
using CanThoTravel.Application.DTOs.Member;
using MediatR;

namespace CanThoTravel.Application.CQRS.Members.Commands
{
    public record RegisterMemberCommand(RegisterRequestDTO Dto) : IRequest<AuthResponseDTO>;
}