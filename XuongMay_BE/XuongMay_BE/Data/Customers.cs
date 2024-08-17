using System.ComponentModel.DataAnnotations;

namespace XuongMay_BE.Data
{
    public class Customers
    {
        [Key]
        public Guid CustomerID { get; set; }
        [Required]
        public string CustomerName { get; set; }

        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
