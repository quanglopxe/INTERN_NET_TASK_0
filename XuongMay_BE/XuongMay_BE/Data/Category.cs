using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XuongMay_BE.Data
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public Guid CategoryID { get; set; }
        [Required]
        public string? CategoryName { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}