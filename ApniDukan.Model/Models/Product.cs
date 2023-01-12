using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApniDukan.Models
{
    [Table(name: "Product", Schema = "Master")]
    public class Product : ModelBase
    {
        [Key]
        public long ProductID { get; set; }

        [Display(Name = "Category")]
        [Range(1, long.MaxValue, ErrorMessage = "The field Category is required.")]
        public long CategoryID { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        [Range(1, double.MaxValue)]
        public decimal Price { get; set; }

        [StringLength(4000)]
        public string FilePath { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }

        public Category Category { get; set; }

        [NotMapped]
        public int Quantity { get; set; } = 1;
    }
}
