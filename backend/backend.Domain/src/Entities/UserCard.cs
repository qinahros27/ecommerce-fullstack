namespace backend.Domain.src.Entities
{
    public class UserCard : BaseEntity
    {
        public string CardName { get; set; }
        public string Type { get; set; } // Mastercard or Visa
        public string CardNumber { get; set; }
        public DateTime ExpiredDate { get; set; }
        public int CVV { get; set; }
        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}