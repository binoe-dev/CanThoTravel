using CanThoTravel.Application.DTOs.Authentication;
using MediatR;

namespace CanThoTravel.Application.CQRS.Authentication.Queries
{
    public record GenerateTokenQuery(GenerateTokenRequestDTO Dto) : IRequest<string?>;
}