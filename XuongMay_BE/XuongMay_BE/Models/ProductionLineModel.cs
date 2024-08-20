using System.ComponentModel.DataAnnotations;

namespace XuongMay_BE.Models
{
    public class ProductionLineModel
    {        

        [Required]
        [StringLength(100)]
        public string LineName { get; set; }

        public Guid? SupervisorID { get; set; }
    }
}
