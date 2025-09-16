namespace CanThoTravel.Application.CQRS.Foods.Commands
{
    using AutoMapper;
    using CanThoTravel.Application.DTOs.Food;
    using CanThoTravel.Application.IRepositories.Food;
    using CanThoTravel.Domain.Entities.Food;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class AddFoodCommandHandler : IRequestHandler<AddFoodCommand, int>
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IMapper _mapper;

        public AddFoodCommandHandler(IFoodRepository foodRepository, IMapper mapper)
        {
            _foodRepository = foodRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddFoodCommand request, CancellationToken cancellationToken)
        {
            var foodEntity = _mapper.Map<FoodEntity>(request.Dto);
            return await _foodRepository.AddAsync(foodEntity);
        }
    }
}