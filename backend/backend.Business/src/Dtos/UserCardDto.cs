using backend.Domain.src.Entities;

namespace backend.Business.src.Dtos
{
    public class UserCardReadDto
    {
        public Guid Id { get; set; }
        public string CardName { get; set; }
        public string Type { get; set; } // Mastercard or Visa
        public string CardNumber { get; set; }
        public DateTime ExpiredDate { get; set; }
        public int UserId { get; set; }
    }

    public class UserCardCreateDto
    {
        public string CardName { get; set; }
        public string Type { get; set; } // Mastercard or Visa
        public string CardNumber { get; set; }
        public DateTime ExpiredDate { get; set; }
        public int CVV { get; set; }
        public int UserId { get; set; }
    }

    public class UserCardUpdateDto
    {
        public string CardName { get; set; }
        public string Type { get; set; } // Mastercard or Visa
        public string CardNumber { get; set; }
        public DateTime ExpiredDate { get; set; }
        public int CVV { get; set; }
    }
}