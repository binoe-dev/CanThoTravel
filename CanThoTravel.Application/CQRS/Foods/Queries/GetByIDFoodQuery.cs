using CanThoTravel.Application.DTOs.Food;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanThoTravel.Application.CQRS.Foods.Queries
{
    public record GetByIDFoodQuery(int Id) : IRequest<FoodResponseDTO>;
}
