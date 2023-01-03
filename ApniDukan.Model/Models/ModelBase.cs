﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ApniDukan.Models
{
    public class ModelBase
    {
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
