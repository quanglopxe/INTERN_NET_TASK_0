using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace XuongMay_BE.Data
{
    public enum Status
    {
        New = 0, Doing = 1, Complete = 2, Cancel = -1
    }    
    public class Task
    {
        [Key]
        public Guid TaskID { get; set; }
        public Guid OrderID { get; set; }
        [ForeignKey("OrderID")]
        public Orders Orders { get; set; }

        public Guid StageID { get; set; }
        [ForeignKey("StageID")]
        public Stage Stages { get; set; }

        public Guid EmpID { get; set; }
        [ForeignKey("EmpID")]
        public Employee Employees { get; set; }

        public Guid SupervisorID { get; set; }
        [ForeignKey("SupervisorID")]
        public Supervisor Supervisors { get; set; }

        [Required]
        public Status Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        
        public string? Remarks { get; set; }


    }
}
