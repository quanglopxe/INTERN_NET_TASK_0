using System.ComponentModel.DataAnnotations;

namespace XuongMay_BE.Models
{
    public class ProductionLineModel
    {
        [Required]
        public int LineID { get; set; }

        [Required]
        [StringLength(100)]
        public string LineName { get; set; }

        public int? SupervisorID { get; set; }
    }
}
