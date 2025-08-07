using CanThoTravel.Application.DTOs.Authentication;
using CanThoTravel.Application.DTOs.Member;
using MediatR;

namespace CanThoTravel.Application.CQRS.Members.Queries
{
    public record LoginQuery(LoginRequestDTO Dto) : IRequest<AuthResponseDTO>;
}