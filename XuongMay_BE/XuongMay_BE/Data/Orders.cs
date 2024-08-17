using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace XuongMay_BE.Data
{
    [Table("Orders")]
    public class Orders
    {
        [Key]
        public Guid OrderID { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public Guid CustomerID { get; set; }

        public int TotalQuantity { get; set; }

        [MaxLength(100)]
        public string Status { get; set; }

        [Required]
        public DateTime DeliveryDate { get; set; }

        //public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}