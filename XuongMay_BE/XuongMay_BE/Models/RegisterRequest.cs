﻿using System.ComponentModel.DataAnnotations;

namespace XuongMay_BE.Models
{
    public class RegisterRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Authorities { get; set; }


    }
}