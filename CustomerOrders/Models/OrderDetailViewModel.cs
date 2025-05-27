namespace CustomerOrders.Web.Models
{
    public class OrderDetailViewModel
    {
        public int OrderID { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
        public ProductsViewModel Products { get; set; } = new();
    }
}
