namespace RightWheel.Domain.Models
{
    public class Part
    {
        public long Id { get; set; }

        public decimal Price { get; set; }

        public List<Category> Categories { get; set; } = new List<Category>();

        public List<Car> Cars { get; set; } = new List<Car>();

        public string Name { get; set; }

        public int Quantity { get; set; }

        public string? Description { get; set; }

        public List<PartImage> Images { get; set; } = new List<PartImage>();

        public Position? Position { get; set;  }
    }
}
