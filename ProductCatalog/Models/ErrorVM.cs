using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.PL.Models
{
    public class ErrorVM
    {
        [Required]
        public int StatusCode { get; set; }
        public string? Message { get; set; }
    }
}
