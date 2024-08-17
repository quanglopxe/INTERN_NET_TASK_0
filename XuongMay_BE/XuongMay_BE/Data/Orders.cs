using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace XuongMay_BE.Data
{
    public class Orders
    {
        [Key]
        public Guid OrderID { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }

        public Guid CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public Customers Customers { get; set; }

        public int TotalQuantity { get; set; }
        public string Status { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}
