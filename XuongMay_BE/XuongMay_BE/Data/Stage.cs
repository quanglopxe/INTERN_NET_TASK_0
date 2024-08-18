using System.ComponentModel.DataAnnotations;

namespace XuongMay_BE.Data
{
    public class Stage
    {
        [Key]
        public Guid StageID { get; set; }
        [Required]
        public string StageName { get; set; }
        public string Description { get; set; }
        public int Sequence { get; set; }
        public ICollection<Task> Tasks { get; set; }

    }
}
