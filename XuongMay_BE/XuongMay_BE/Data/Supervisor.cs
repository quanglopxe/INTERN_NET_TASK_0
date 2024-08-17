using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XuongMay_BE.Data
{
    public class Supervisor
    {
        [Key]
        public Guid SupervisorID { get; set; }
        [Required]
        public string SupervisorName { get; set; }
        public int LineID { get; set; }
    }
}
