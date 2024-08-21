using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XuongMay_BE.Data
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        public Guid CustomerID { get; set; }

        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; }

        [StringLength(255)]
        public string? Phone { get; set; }

        [StringLength(255)]
        public string? Address { get; set; }
        public Guid? UserID { get; set; }
        [ForeignKey("UserID")]
        public User Users { get; set; }
    }
}
