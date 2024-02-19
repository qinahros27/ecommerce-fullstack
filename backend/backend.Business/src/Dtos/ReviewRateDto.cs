using backend.Domain.src.Entities;

namespace backend.Business.src.Dtos
{
    public class ReviewRateReadDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public string? Review { get; set; }
        public int? RatePoint { get; set; }
    }

    public class ReviewRateCreateDto
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public string? Review { get; set; }
        public int? RatePoint { get; set; }
    }

    public class ReviewRateUpdateDto
    {
        public string? Review { get; set; }
        public int? RatePoint { get; set; }
    }

    public class ProductReviewRateReadDto
    {
        public UserReadDto User { get; set; }
        public string? Review { get; set; }
        public int? RatePoint { get; set; }
    }

    public class UserReviewRateReadDto
    {
        public ProductReadDto Product { get; set; }
        public string? Review { get; set; }
        public int? RatePoint { get; set; }
    }
}