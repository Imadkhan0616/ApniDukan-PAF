using ApniDukan.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApniDukan.Model.Models
{
    [Table(name: "Role", Schema = "Admin")]
    public class Role : ModelBase
    {
        [Key]
        public long RoleID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
