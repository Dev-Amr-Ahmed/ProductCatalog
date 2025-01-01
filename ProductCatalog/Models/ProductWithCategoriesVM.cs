namespace ProductCatalog.PL.Models
{
    public class ProductWithCategoriesVM
    {
        public ProductVM product { get; set; }
        public List<CategoryVM> categories { get; set; } = new();
    }
}
