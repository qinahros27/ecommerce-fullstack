namespace backend.Domain.src.Entities
{
    public class ReviewRate : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set;}
        public string? Review { get; set; }
        public int? RatePoint { get; set; }

        public User User { get; set; }
        public Product Product { get; set; }
    }
}