using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.PL.Models
{
    public class CategoryVM
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        public bool IsSelected { get; set; }
    }
}
