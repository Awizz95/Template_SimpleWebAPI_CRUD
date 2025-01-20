namespace Template_SimpleWebAPI_CRUD.Models.DTO.Products
{
    public class ProductCreateRequestDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public Guid CategoryId { get; set; }
    }
}
