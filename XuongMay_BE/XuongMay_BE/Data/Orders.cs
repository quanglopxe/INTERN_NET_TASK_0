using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace XuongMay_BE.Data
{
    public enum Statuss
    {
        New = 0, Payment = 1, Complete = 2, Cancel = -1
    }

    [Table("Orders")]
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

        [MaxLength(100)]
        public string Status { get; set; }

        [Required]
        public DateTime DeliveryDate { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        public Orders()
        {
            OrderDetails = new List<OrderDetail>();
        }
    }
}

