using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerOrders.DataAccess.Models
{
    [Table("Order Details")]
    public class OrderDetails
    {
        [Required]
        public int OrderID { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        public short Quantity { get; set; }

        public float Discount { get; set; }

        public Orders Order { get; set; }

        public Products Products { get; set; }
    }
}