using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AlgoFit.Data.Models;

namespace AlgoFit.Data.DTO
{
    public class SignUpDTO
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name must be less than 50 characters")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "LastName must be less than 50 characters")]
        public string LastName { get; set; }

        [Required]
        [Phone(ErrorMessage = "Phone is not valid")]
        public string Phone { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [Required]
        [Compare("Email", ErrorMessage = "Confirm Email is not matching the Email")]
        public string ConfirmEmail { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "Password must be less than 250 characters")]
        public string Password { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "Confirm Password must be less than 250 characters")]
        [Compare("Password", ErrorMessage = "Confirm Password is not matching the Password")]
        public string ConfirmPassword { get; set; }
    }
    public class LoginDTO
    {
        [Required]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        [Required]
        [MaxLength(250)]
        public string Password { get; set; }
    }
    public class ProfileDTO
    {
        public Guid Id { get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public string Type { get; set; }
        public Diet Diet { get; set;}
    }
    public class UserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public string Type { get; set; }
    }
}