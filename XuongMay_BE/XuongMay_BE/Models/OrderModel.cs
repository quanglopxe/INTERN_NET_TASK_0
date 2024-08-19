using System.ComponentModel.DataAnnotations;
using XuongMay_BE.Data;

namespace XuongMay_BE.Models
{

    public class OrderModel
    {
        [Required]
        public DateTime OrderDate { get; set; }

        public Guid CustomerID { get; set; }

        public int TotalQuantity { get; set; }
        public Statuss Status { get; set; }

        [Required]
        public DateTime DeliveryDate { get; set; }
    }
}