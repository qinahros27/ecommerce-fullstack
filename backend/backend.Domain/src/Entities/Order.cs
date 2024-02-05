namespace backend.Domain.src.Entities
{
    public class Order : BaseEntity
    {
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid UserId { get; set; }

        public User User { get; set; }
        public Shipment Shipment { get; set; }
        public Payment Payment { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
    }
}