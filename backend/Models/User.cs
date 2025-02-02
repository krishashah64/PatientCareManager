using System;
using System; // For general .NET system classes
using System.Linq; // For LINQ
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;


namespace Backend
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }

        [NotMapped] // ignores this property
        public string Password { get; set; }

        public string? PasswordHash { get; set; }  
        public string? PasswordSalt { get; set; } 

        public DateTime? LastLoginDate { get; set; }  // To track last login
        public int FailedLoginAttempts { get; set; }  // To track failed login attempts
        public string AccountStatus { get; set; } // E.g., "Active", "Locked", "Deactivated"
        public bool IsEmailVerified { get; set; } // To track if the user's email is verified
        public DateTime CreatedAt { get; set; } // When the user account was created
        public DateTime UpdatedAt { get; set; } // When the user account was last updated

        public enum AccountStatusEnum
        {
            Active,
            Locked,
            Deactivated
        }
    }
}
