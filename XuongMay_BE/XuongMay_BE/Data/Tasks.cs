using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace XuongMay_BE.Data
{
    public class Tasks
    {
        [Key]
        public Guid TaskID { get; set; }

        public Guid OrderID { get; set; }
        [ForeignKey("OrderID")]
        public Orders Orders { get; set; }

        public Guid StageID { get; set; }
        [ForeignKey("StageID")]
        public Stage Stage { get; set; }

        public int AssignedTo { get; set; }
        [ForeignKey("EmpID")]
        public Employee Employee { get; set; }

        public int AssignedBy { get; set; }
        [ForeignKey("SupervisorID")]
        public Supervisor Supervisor { get; set; }

        [Required]
        public string Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        
        public string Remarks { get; set; }
    }
}
