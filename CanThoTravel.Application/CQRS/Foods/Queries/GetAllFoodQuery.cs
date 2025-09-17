
using CanThoTravel.Application.DTOs.Food;
using MediatR;

namespace CanThoTravel.Application.CQRS.Foods.Queries
{
    public record GetAllFoodQuery() : IRequest<List<FoodResponseDTO>>;
}