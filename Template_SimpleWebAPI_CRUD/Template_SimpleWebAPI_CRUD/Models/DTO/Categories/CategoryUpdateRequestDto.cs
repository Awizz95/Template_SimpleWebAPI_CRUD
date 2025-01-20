namespace Template_SimpleWebAPI_CRUD.Models.DTO.Categories
{
    public class CategoryUpdateRequestDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
