using App.Repositories.Categories;

namespace App.Repositories.Products
{
    public class Product : BaseEntity<long>, IAuditEntity
    {
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public long CategoryId { get; set; }
        public Category Category { get; set; } = default!;
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
