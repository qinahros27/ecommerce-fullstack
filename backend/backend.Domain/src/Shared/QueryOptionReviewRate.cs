namespace backend.Domain.src.Shared
{
    public class QueryOptionReviewRate : QueryOptions
    {
        public Guid? ProductId { get; set; } =Guid.Empty;
        public Guid? UserId { get; set; } = Guid.Empty;
    }
}