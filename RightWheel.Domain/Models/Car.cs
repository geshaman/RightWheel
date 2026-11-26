namespace RightWheel.Domain.Models
{
    public class Car
    {
        public long Id { get; set; }

        public string Brand { get; set; }

        public string Model {  get; set; }

        public string Engine { get; set; }

        public List<Part> Parts { get; set; }
    }
}
