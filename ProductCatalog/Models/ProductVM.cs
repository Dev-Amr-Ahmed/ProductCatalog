using ProductCatalog.DAL.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProductCatalog.PL.Models
{
    public class ProductVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("Creation Date")]
        public DateTime CreationDate { get; set; }

        [Required]
        public string Creator { get; set; }

        [Required]
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 1000_000_000_000, ErrorMessage = "Price must be greater than zero")]
        public decimal Price { get; set; }

        [Required]
        public Category Category { get; set; }
    }
}
