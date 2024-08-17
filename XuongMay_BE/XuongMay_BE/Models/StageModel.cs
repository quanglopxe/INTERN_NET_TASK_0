using System.ComponentModel.DataAnnotations;

namespace XuongMay_BE.Models
{
    public class StageModel
    {
        [Required]
        public string StageName { get; set; }
        public string Description { get; set; }
        public int Sequence { get; set; }
    }
}
