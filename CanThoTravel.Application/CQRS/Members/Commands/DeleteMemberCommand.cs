using MediatR;

namespace CanThoTravel.Application.CQRS.Members.Commands
{
    public record DeleteMemberCommand(int Id) : IRequest<bool>;
}