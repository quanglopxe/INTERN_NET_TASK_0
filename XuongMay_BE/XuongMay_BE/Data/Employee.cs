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
        [StringLength(100)]
        public string EmpName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public Guid LineID { get; set; }
        [ForeignKey("LineID")]
        public ProductionLine ProductionLines {  get; set; }

        public ICollection<Task> Tasks { get; set; }

    }
}
