﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XuongMay_BE.Data
{
    [Table("ProductionLine")]
    public class ProductionLine
    {
        [Key]
        public int LineID { get; set; }

        [Required]
        [StringLength(100)]
        public string LineName { get; set; }

        public int? SupervisorID { get; set; }

        [ForeignKey("SupervisorID")]
        public Supervisor? Supervisor { get; set; }
    }
}
