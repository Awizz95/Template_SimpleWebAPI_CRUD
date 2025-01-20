namespace Template_SimpleWebAPI_CRUD.Models.DTO.Categories
{
    public record CategoryResponseDto
    (
        Guid Id,
        string Name,
        string Description
    );
}
