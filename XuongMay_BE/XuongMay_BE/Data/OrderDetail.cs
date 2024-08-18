using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XuongMay_BE.Data
{
    public class OrderDetail
    {
        public Guid OrderID { get; set; }
        public Guid ProductID { get; set; }
        public Guid SupervisorID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }

        //[ForeignKey("OrderID")]
        //public Orders Orders { get; set; }

        //[ForeignKey("ProductID")]
        //public Product Product { get; set; }

        public Orders Orders { get; set; }

        public Product Product { get; set; }

        public Supervisor Supervisor { get; set; }

    }
}