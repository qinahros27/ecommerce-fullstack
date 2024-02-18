namespace backend.Domain.src.Entities
{
    public class OrderProduct : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string? Color { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
        
    }
}