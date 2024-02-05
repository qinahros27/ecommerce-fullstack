using System.Diagnostics.Contracts;

namespace backend.Domain.src.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

       public List<Product> Products { get; set; }
    }
}