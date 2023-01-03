﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApniDukan.Models
{
    [Table(name: "User",Schema = "Admin")]
    public class User : ModelBase
    {
        [Key]
        public long UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required, StringLength(200)]
        [EmailAddress]
        public string Email { get; set; }

        [Required,StringLength(100)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }
    }
}
