namespace Template_SimpleWebAPI_CRUD.Models
{
    public class Product : Base
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }

        // Relationship
        public Guid CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
