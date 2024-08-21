using System.ComponentModel.DataAnnotations;

namespace XuongMay_BE.Models
{
    public class SupervisorModel
    {        

        [Required]
        [StringLength(100)]
        public string SupervisorName { get; set; }
        
        public Guid? UserID { get; set; }
    }
}
