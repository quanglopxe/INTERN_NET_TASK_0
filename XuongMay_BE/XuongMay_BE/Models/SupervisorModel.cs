using System.ComponentModel.DataAnnotations;

namespace XuongMay_BE.Models
{
    public class SupervisorModel
    {
        [Required]
        public int SupervisorID { get; set; }

        [Required]
        [StringLength(100)]
        public string SupervisorName { get; set; }

        [Required]
        public Guid? UserID { get; set; }
    }
}
