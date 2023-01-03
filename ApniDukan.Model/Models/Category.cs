using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApniDukan.Models
{
    [Table(name: "Category", Schema = "Master")]
    public class Category : ModelBase
    {
        [Key]
        public long CategoryID { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
