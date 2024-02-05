using backend.Domain.src.Entities;

namespace backend.Business.src.Dtos
{
    public class OrderProductReadDto
    {
        public Guid Id { get; set; }
        public Order OrderId { get; set; }
        public Product ProductId { get; set; }
        public int Quantity { get; set; }
        public string Color { get; set; }
    }

    public class OrderProductCreateDto
    {
        public Order OrderId { get; set; }
        public Product ProductId { get; set; }
        public int Quantity { get; set; }
        public string Color { get; set; }
    }

    public class OrderProductUpdateDto
    {
        public int Quantity { get; set; }
        public string Color { get; set; }
    }
}