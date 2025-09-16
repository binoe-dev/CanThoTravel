using AutoMapper;
using CanThoTravel.Application.DTOs.Food;
using CanThoTravel.Domain.Entities.Food;

namespace CanThoTravel.Application.Mappings
{
    public class FoodProfile : Profile
    {
        public FoodProfile()
        {
            CreateMap<FoodEntity, FoodResponseDTO>();
            CreateMap<FoodResponseDTO, FoodEntity>();

        }
    }
}
