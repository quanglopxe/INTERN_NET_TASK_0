﻿using System.ComponentModel.DataAnnotations;

namespace XuongMay_BE.Models
{
    public class CustomerModel
    {
        [Required]
        public Guid CustomerID { get; set; }

        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; }

        [StringLength(255)]
        public string? Phone { get; set; }

        [StringLength(255)]
        public string? Address { get; set; }        
        public Guid? UserID { get; set; }
    }
}
