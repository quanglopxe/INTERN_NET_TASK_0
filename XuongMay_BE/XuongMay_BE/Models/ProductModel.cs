using System.ComponentModel.DataAnnotations;
using XuongMay_BE.Data;

namespace XuongMay_BE.Models
{
    public class ProductModel
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
        public string? Description { get; set; }
        public Guid CategoryID { get; set; }
    }
}
