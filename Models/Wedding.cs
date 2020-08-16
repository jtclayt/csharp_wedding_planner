using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using WeddingPlanner.CustomValidation;

namespace WeddingPlanner.Models
{
    public class Wedding
    {
        [Key]
        public int WeddingId { get; set; }

        [Required]
        [MinLength(2, ErrorMessage="Wedder One must be 2 or more characters.")]
        [Display(Name="Wedder One")]
        public string WedderOne { get; set; }

        [Required]
        [MinLength(2, ErrorMessage="Wedder Two must be 2 or more characters.")]
        [Display(Name="Wedder Two")]
        public string WedderTwo { get; set; }

        [Required]
        [FutureDate]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        [Required]
        [MinLength(10, ErrorMessage="Address must be 10 or more characters.")]
        public string Address { get; set; }

        public int UserId { get; set; }
        public User Creator { get; set; }

        public List<Rsvp> Attendees { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
