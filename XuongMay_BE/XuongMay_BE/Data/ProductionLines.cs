using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XuongMay_BE.Data
{
    public class ProductionLines
    {
        [Key]
        public Guid LineID { get; set; }
        [Required]
        public string LineName { get; set; }

        public Guid SupervisorID { get; set; }
        [ForeignKey("SupervisorID")]
        public Supervisor Supervisor { get; set; }
    }
}
