using System.Reflection.Metadata;
using CanThoTravel.Application.DTOs.Food;
using MediatR;

namespace CanThoTravel.Application.CQRS.Foods.Commands;

public record UpdateFoodCommand(UpdateFoodRequestDTO Dto) : IRequest<bool>;