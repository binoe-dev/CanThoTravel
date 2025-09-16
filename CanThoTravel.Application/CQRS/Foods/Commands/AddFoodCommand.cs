
using CanThoTravel.Application.DTOs.Food;
using MediatR;

namespace CanThoTravel.Application.CQRS.Foods.Commands;

public record AddFoodCommand(AddFoodRequestDTO Dto) : IRequest<int>;