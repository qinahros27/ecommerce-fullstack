using backend.Domain.src.Entities;

namespace backend.Business.src.Dtos
{
    public class OrderProductReadDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string? Color { get; set; }
    }

    public class OrderProductCreateDto
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string? Color { get; set; }
    }

    public class OrderProductUpdateDto
    {
        public int Quantity { get; set; }
        public string? Color { get; set; }
    }

    public class OrderOfOrderProductReadDto
    {
        public ProductReadDto Product { get; set; }
        public int Quantity { get; set; }
        public string? Color { get; set; }
        public OrderProductShipmentReadDto Shipment { get; set; }
    }
}