using backend.Domain.src.Entities;

namespace backend.Business.src.Dtos
{
    public class ReviewRateReadDto
    {
        public Guid Id { get; set; }
        public User UserId { get; set; }
        public Product ProductId { get; set; }
        public string Review { get; set; }
        public int RatePoint { get; set; }
    }

    public class ReviewRateCreateDto
    {
        public User UserId { get; set; }
        public Product ProductId { get; set; }
        public string Review { get; set; }
        public int RatePoint { get; set; }
    }

    public class ReviewRateUpdateDto
    {
        public string Review { get; set; }
        public int RatePoint { get; set; }
    }
}