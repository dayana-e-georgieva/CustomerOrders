namespace CustomerOrders.Web.Models
{
    public class CustomerWithOrderCountViewModel
    {
        public string CustomerID { get; set; }
        public string ContactName { get; set; }
        public int OrderCount { get; set; }
    }
}
