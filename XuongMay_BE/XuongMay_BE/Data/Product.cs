using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XuongMay_BE.Data
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public Guid ProductID { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(0,double.MaxValue)]
        public double Price { get; set; }
        public string? Description { get; set; }
        public Guid CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public Category Category { get; set; }
    }
}
