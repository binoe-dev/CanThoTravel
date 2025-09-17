using CanThoTravel.Application.IRepositories.Food;
using MediatR;

namespace CanThoTravel.Application.CQRS.Foods.Commands
{
    public class DeleteFoodCommandHandler : IRequestHandler<DeleteFoodCommand, bool>
    {
        private readonly IFoodRepository _foodRepository;

        public DeleteFoodCommandHandler(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        public async Task<bool> Handle(DeleteFoodCommand request, CancellationToken cancellationToken)
        {
            await _foodRepository.DeleteAsync(request.Id);
            return true;
        }
    }
}