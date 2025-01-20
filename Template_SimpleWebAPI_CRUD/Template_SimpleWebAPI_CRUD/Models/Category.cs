namespace Template_SimpleWebAPI_CRUD.Models
{
    public class Category : Base
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
