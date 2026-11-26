namespace RightWheel.Domain.Models
{
    public class Order
    {
        public long Id { get; set; }
        
        public string ClientPhoneNumber { get; set; }

        public string ClientName { get; set; }

        public string? VIN { get; set; }
        
        public Status Status { get; set; }

        public DateTime OrderCreateTime {  get; set; }

        public DateTime OrderEndTime { get; set; }

        public List<OrderItem> Items { get; set; }
    }
}
