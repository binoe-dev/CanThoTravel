using CanThoTravel.Domain.Entities.Food;

namespace CanThoTravel.Application.IRepositories.Food
{
    public interface IFoodRepository
    {
        Task<List<FoodEntity>> GetAllAsync();
        Task<FoodEntity?> GetByIdAsync(int id);
        Task<int> AddAsync(FoodEntity food);
        Task UpdateAsync(FoodEntity food);
        Task DeleteAsync(int id);
    }
}
