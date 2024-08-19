using System.ComponentModel.DataAnnotations;

namespace XuongMay_BE.Models
{
    public class OrderDetailModel
    {
        public Guid OrderID { get; set; }
        public Guid ProductID { get; set; }                

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double Price { get; set; }        
    }
}