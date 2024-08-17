using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XuongMay_BE.Data
{
    [Table("OrderDetail")]
    //[Keyless]
    public class OrderDetail
    {
        [Key]
        public Guid OrderDetailID { get; set; }
        public Guid OrderID { get; set; }
        public Guid ProductID { get; set; }
        public Guid SupervisorID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }

        //[ForeignKey("OrderID")]
        //public Orders Orders { get; set; }

        //[ForeignKey("ProductID")]
        //public Product Product { get; set; }

    }
}