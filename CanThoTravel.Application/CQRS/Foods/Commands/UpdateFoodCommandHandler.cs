using AutoMapper;
using CanThoTravel.Application.IRepositories.Food;
using CanThoTravel.Domain.Entities.Food;
using MediatR;

namespace CanThoTravel.Application.CQRS.Foods.Commands
{
    public class UpdateFoodCommandHandler : IRequestHandler<UpdateFoodCommand, bool>
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IMapper _mapper;

        public UpdateFoodCommandHandler(IFoodRepository foodRepository, IMapper mapper)
        {
            _foodRepository = foodRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateFoodCommand request, CancellationToken cancellationToken)
        {
            var foodEntity = _mapper.Map<FoodEntity>(request.Dto);
            await _foodRepository.UpdateAsync(foodEntity);
            return true;
        }
    }
}