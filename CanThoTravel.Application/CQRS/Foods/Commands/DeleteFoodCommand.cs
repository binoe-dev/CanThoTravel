using MediatR;

namespace CanThoTravel.Application.CQRS.Foods.Commands
{
    public record DeleteFoodCommand(int Id) : IRequest<bool>;
}