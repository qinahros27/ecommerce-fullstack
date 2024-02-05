namespace backend.Domain.src.Shared
{
    public class QueryOptionProduct : QueryOptions
    {
        public int? MinPrice { get; set; } = -1;
        public int? MaxPrice { get; set; } = -1;
        public Guid? CategoryId { get; set; } = Guid.Empty;
    }
}