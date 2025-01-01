using ProductCatalog.DAL.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using NuGet.Packaging.Signing;
using System;

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

        private TimeSpan _duration;
        [Required]
        public TimeSpan Duration
        {
            get
            {
                _duration = new TimeSpan(days: DurationDays, hours: DurationHours, 0, 0);
                return _duration;
            }
            set
            {
                DurationHours = value.Hours;
                DurationDays = value.Days;
                _duration = value;
            }
        }

        [Required]
        public int DurationDays { get; set; }

        [Required]
        public int DurationHours { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 1000_000_000_000, ErrorMessage = "Price must be greater than zero")]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
