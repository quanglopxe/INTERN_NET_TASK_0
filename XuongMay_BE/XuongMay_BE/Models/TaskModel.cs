using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using XuongMay_BE.Data;

namespace XuongMay_BE.Models
{
    public class TaskModel
    {
        public Guid OrderID { get; set; }                

        public Guid StageID { get; set; }                

        public Guid AssignedTo { get; set; }                

        public Guid AssignedBy { get; set; }                

        [Required]
        public string Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public string Remarks { get; set; }
    }
}
