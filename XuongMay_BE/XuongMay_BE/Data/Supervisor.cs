using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XuongMay_BE.Data
{
    [Table("Supervisor")]
    public class Supervisor
    {
        [Key]
        public Guid SupervisorID { get; set; }

        [Required]
        [StringLength(100)]
        public string SupervisorName { get; set; }

        public int? LineID { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        public Supervisor()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
    }
}
