using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.DAL.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public string Creator { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public long Duration { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 1000_000_000_000, ErrorMessage = "Price must be greater than zero")]
        public decimal Price { get; set; }

        [Required]
        public virtual Category Category { get; set; }

        public DateTime EndDate {  get; set; }

        [Required]
        public bool IsDeleted { get; set; }

    }
}
