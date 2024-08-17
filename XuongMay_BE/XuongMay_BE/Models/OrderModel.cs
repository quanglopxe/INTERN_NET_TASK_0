using System.ComponentModel.DataAnnotations;

namespace XuongMay_BE.Models
{
    public class OrderModel
    {
        [Required]
        public DateTime OrderDate { get; set; }

        public Guid CustomerID { get; set; }

        public int TotalQuantity { get; set; }

        [MaxLength(100)]
        public string Status { get; set; }

        [Required]
        public DateTime DeliveryDate { get; set; }
    }
}