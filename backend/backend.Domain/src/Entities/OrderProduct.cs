namespace backend.Domain.src.Entities
{
    public class OrderProduct : BaseEntity
    {
        public Order Order { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}