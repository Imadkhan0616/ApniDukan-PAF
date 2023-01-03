using ApniDukan.Validation.Attribute;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApniDukan.Models
{
    [Table(name: "Order", Schema = "Cart")]
    public class Order : ModelBase
    {
        [Key]
        public long OrderID { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "The field Customer is required.")]
        public long CustomerID { get; set; }
        public long CouponID { get; set; }

        [DateRequired(ErrorMessage = "The field Order Date is required.")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [AmountRequired(ErrorMessage = "The field Total Amount is required.")]
        public decimal TotalAmount { get; set; }

        [AmountRequired(ErrorMessage = "The field Net Amount is required.")]
        public decimal NetAmount { get; set; }
        public decimal? DiscountAmount { get; set; }

        public Customer Customer { get; set; }
    }
}
