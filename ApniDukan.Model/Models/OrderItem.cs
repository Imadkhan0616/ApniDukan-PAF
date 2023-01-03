using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApniDukan.Models
{
    [Table(name: "OrderItem", Schema = "Cart")]
    public class OrderItem :ModelBase
    {
        [Key]
        public long OrderItemID { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "The field Order is required.")]
        public long OrderID { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "The field Product is required.")]
        public long ProductID { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The field Quantity is required.")]
        public int Quantity { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "The field Item Total is required.")]
        public decimal ItemTotalAmount { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}