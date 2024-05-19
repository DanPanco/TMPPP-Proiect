namespace Store.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
