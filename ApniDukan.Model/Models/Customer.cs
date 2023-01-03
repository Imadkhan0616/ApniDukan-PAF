﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApniDukan.Models
{
    [Table(name: "Customer", Schema = "Cart")]
    public class Customer :ModelBase
    {
        public long CustomerID{ get; set; }

        [Required, StringLength(50)]
        public string FirstName { get; set; }
        [Required, StringLength(50)]
        public string LastName { get; set; }
        [Required, StringLength(500)]
        public string Address { get; set; }
        
        [Required, StringLength(100)]
        public string City { get; set; }
        
        [Required, StringLength(100)]
        public string Country { get; set; }
        
        [Required, StringLength(20)]
        public string ContactNo { get; set; }
        
        [Required, StringLength(200)]
        [EmailAddress]
        public string Email { get; set; }
    }
}