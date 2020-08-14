using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models
{
    public class User
    {
        [Key]
        public int Id { get ; set; }

        [Required]
        [MinLength(2, ErrorMessage="First name must be 2 or more characters")]
        [Display(Name="First Name")]
        public string FirstName { get ; set; }

        [Required]
        [MinLength(2, ErrorMessage="Last name must be 2 or more characters")]
        [Display(Name="Last Name")]
        public string LastName { get ; set; }

        [EmailAddress]
        [Required]
        public string Email { get ; set; }

        [Required]
        [MinLength(8, ErrorMessage="Password must be 8 characters or longer!")]
        [DataType(DataType.Password)]
        public string Password { get ; set; }

        // Will not be mapped to your users table!
        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Name="Confirm Password")]
        public string Confirm { get ; set; }

        public DateTime CreatedAt { get ; set; } = DateTime.Now;
        public DateTime UpdatedAt { get ; set; } = DateTime.Now;
    }
}