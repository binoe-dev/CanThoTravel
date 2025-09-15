
using CanThoTravel.Application.IRepositories.Food;
using CanThoTravel.Application.Repository.PostgreSQL;
using CanThoTravel.Domain.Entities.Food;
using CanThoTravel.Infrastructure.Abstracts;
using Npgsql;

namespace CanThoTravel.Infrastructure.Repositories.Food
{
    public class FoodRepository : PostgresFunctionBase<FoodEntity>, IFoodRepository
    {
        public FoodRepository(NpgsqlConnection connection, ITransactionManager transactionManager) : base(connection, transactionManager)
        {
        }

        public Task<int> AddAsync(FoodEntity food)
        {
            var parameters = new Dictionary<string, object>
            {
                { "p_name", food.Name },
                { "p_description", food.Description },
                { "p_image_url", food.ImageUrl }
            };
            return ExecuteNonQueryFunctionAsync("masterdata.add_food", parameters);
        }

        public async Task<List<FoodEntity>> GetAllAsync()
        {
            var parameters = new Dictionary<string, object>();
            var result = await ExecuteFunctionWithCursorAsync<FoodEntity>("masterdata.get_all_foods", parameters);
            return result.ToList();
        }

        public async Task<FoodEntity> GetByIdAsync(int id)
        {
            var parameters = new Dictionary<string, object>
            {
                { "p_id", id }
            };
            var data = await ExecuteFunctionWithCursorAsync<FoodEntity>("masterdata.get_food_by_id", parameters);
            return data.FirstOrDefault()!;
        }

        public Task UpdateAsync(FoodEntity food)
        {
            var parameters = new Dictionary<string, object>
            {
                { "p_id", food.Id },
                { "p_name", food.Name },
                { "p_description", food.Description },
                { "p_image_url", food.ImageUrl }
            };
            return ExecuteNonQueryFunctionAsync("masterdata.update_food", parameters);
        }

        public Task DeleteAsync(int id)
        {
            var parameters = new Dictionary<string, object>
            {
                { "p_id", id }
            };
            return ExecuteNonQueryFunctionAsync("masterdata.delete_food", parameters);
        }
    }
}