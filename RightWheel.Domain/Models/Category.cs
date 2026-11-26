namespace RightWheel.Domain.Models
{
    public class Category
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long? ParentCategoryId { get; set; }

        public Category ParentCategory { get; set; }
    }
}
