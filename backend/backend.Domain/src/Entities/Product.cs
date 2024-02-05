namespace backend.Domain.src.Entities
{
    public class Product : BaseEntity
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public float Price { get; set; }
        public List<string> Images { get; set; }
        public int Inventory { get; set; }
        public Guid CategoryId { get; set; }

        public Category Category { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
        public List<ReviewRate> ReviewRates { get; set; }
    }
}