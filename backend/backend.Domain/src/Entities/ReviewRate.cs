namespace backend.Domain.src.Entities
{
    public class ReviewRate : BaseEntity
    {
        public User User { get; set; }
        public Product Product { get; set; }
        public string? Review { get; set; }
        public int? RatePoint { get; set; }
    }
}