using backend.Domain.src.Entities;

namespace backend.Business.src.Dtos
{
    public class PaymentReadDto
    {
        public Guid Id { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public float TotalPrice { get; set; }
        public Guid OrderId { get; set; }
    }

    public class PaymentCreateDto
    {
        public PaymentMethod PaymentMethod { get; set; }
        public float TotalPrice { get; set; }
        public Guid OrderId { get; set; }
    }

    public class PaymentUpdateDto
    {
        public PaymentMethod PaymentMethod { get; set; }
        public float TotalPrice { get; set; }
    }
}