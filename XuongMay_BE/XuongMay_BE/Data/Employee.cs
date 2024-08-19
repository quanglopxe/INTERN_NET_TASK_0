using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XuongMay_BE.Data
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public Guid EmpID { get; set; }
        [Required]
        public string EmpName { get; set; }
        public Guid LineID { get; set; }
        [ForeignKey("LineID")]
        public Guid UserID { get; set; }
        [ForeignKey("UserID")]
        public User Users { get; set; }
        public ProductionLine ProductionLines {  get; set; }

        public ICollection<Task> Tasks { get; set; }

    }
}
