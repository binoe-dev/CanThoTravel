using AutoMapper;
using CanThoTravel.Application.DTOs.Food;
using CanThoTravel.Application.IRepositories.Food;
using MediatR;

namespace CanThoTravel.Application.CQRS.Foods.Queries
{
    public class GetAllFoodQueryHandler : IRequestHandler<GetAllFoodQuery, List<FoodResponseDTO>>
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IMapper _mapper;

        public GetAllFoodQueryHandler(IFoodRepository foodRepository, IMapper mapper)
        {
            _foodRepository = foodRepository;
            _mapper = mapper;
        }

        public async Task<List<FoodResponseDTO>> Handle(GetAllFoodQuery request, CancellationToken cancellationToken)
        {
            var foods = await _foodRepository.GetAllAsync();
            return _mapper.Map<List<FoodResponseDTO>>(foods);
        }
    }
}