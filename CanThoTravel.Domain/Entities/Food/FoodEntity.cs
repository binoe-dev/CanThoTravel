namespace CanThoTravel.Domain.Entities.Food
{
    public class FoodEntity : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
