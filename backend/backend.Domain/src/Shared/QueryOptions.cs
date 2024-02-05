namespace backend.Domain.src.Shared
{
    public class QueryOptions
    {
        public string Search { get; set; } = string.Empty;
        public string Order { get; set; } = string.Empty;
        public bool OrderByDescending { get; set; } = false;
        public int Offset { get; set; } = 0;
        public int Limit { get; set; } = 10;
    }
}