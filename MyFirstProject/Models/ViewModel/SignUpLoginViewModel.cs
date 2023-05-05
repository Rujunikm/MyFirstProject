﻿using System.ComponentModel.DataAnnotations;

namespace MyFirstProject.Models
{
    public class SignUpLoginViewModel
    {
        public string Username { get; set; } = string.Empty;
        public string Fname { get; set; } = string.Empty;
        public string Lname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public long MobileNo { get; set; }
        public string State { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        //[DisplayFormat(DisplayFormateString="{0:dd\mm\yyyy}")]
        public DateTime DOB { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Adress { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

    }
}
