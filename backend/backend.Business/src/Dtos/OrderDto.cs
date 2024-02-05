using backend.Domain.src.Entities;

namespace backend.Business.src.Dtos
{
    public class OrderReadDto
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public Guid UserId { get; set; }
    }

    public class OrderCreateDto
    {
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public Guid UserId { get; set; }
    }

    public class OrderUpdateDto
    {
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
    }
}