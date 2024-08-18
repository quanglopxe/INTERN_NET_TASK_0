using System.ComponentModel.DataAnnotations;

namespace XuongMay_BE.Models
{

    public enum Statuss
    {
        New = 0, Payment = 1, Complete = 2, Cancel = -1
    }
    public class OrderModel
    {
        [Required]
        public DateTime OrderDate { get; set; }

        public Guid CustomerID { get; set; }

        public int TotalQuantity { get; set; }
        public string Status { get; set; }

        [Required]
        public DateTime DeliveryDate { get; set; }
    }
}