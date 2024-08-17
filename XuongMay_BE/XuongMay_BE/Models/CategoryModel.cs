using System.ComponentModel.DataAnnotations;

namespace XuongMay_BE.Models
{
    public class CategoryModel
    {
        [Required]
        public string CategoryName { get; set; }
    }
}
