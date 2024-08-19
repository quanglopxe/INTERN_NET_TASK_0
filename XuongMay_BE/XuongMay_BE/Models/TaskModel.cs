using System.ComponentModel.DataAnnotations;
using XuongMay_BE.Data;

namespace XuongMay_BE.Models
{
    public class TaskModel
    {
        [Required]
        public Guid OrderID { get; set; }
        [Required]
        public Guid StageID { get; set; }
        [Required]
        public Guid EmpID { get; set; }
        [Required]
        public Guid SupervisorID { get; set; }
        [Required]
        public Status Status { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }

        public string? Remarks { get; set; }
    }
}