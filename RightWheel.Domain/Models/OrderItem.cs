namespace RightWheel.Domain.Models
{
    public class OrderItem
    {
        public long OrderId { get; set; }

        public long PartId { get; set; }
        
        public int PartQuantity { get; set; }

        public decimal PriceAtOrder { get; set; }
    }
}
