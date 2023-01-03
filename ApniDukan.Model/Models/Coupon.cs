using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApniDukan.Models
{
    [Table(name: "Coupon", Schema = "Cart")]
    public class Coupon:ModelBase
    {
        [Key]
        public long CouponID { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(50)]
        public string Code { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        public int DiscountPercent { get; set; }
    }
}