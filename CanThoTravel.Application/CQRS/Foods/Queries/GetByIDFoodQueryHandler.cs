using AutoMapper;
using CanThoTravel.Application.DTOs.Food;
using CanThoTravel.Application.IRepositories.Food;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanThoTravel.Application.CQRS.Foods.Queries
{
    public class GetByIDFoodQueryHandler : IRequestHandler<GetByIDFoodQuery, FoodResponseDTO>
    {
        private readonly IMapper _mapper;
        private readonly IFoodRepository _foodRepository;
        public GetByIDFoodQueryHandler(IMapper mapper, IFoodRepository foodRepository)
        {
            _mapper = mapper;
            _foodRepository = foodRepository;
        }
        
        public async Task<FoodResponseDTO> Handle(GetByIDFoodQuery request, CancellationToken cancellationToken)
        {
            var food = await _foodRepository.GetByIdAsync(request.Id);
            return _mapper.Map<FoodResponseDTO>(food);
        }
    }
}
