using Template_SimpleWebAPI_CRUD.Models;

namespace Template_SimpleWebAPI_CRUD.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> GetCategoryByIdAsync(Guid id, CancellationToken ct);
        Task<Category> GetCategoryByNameAsync(string name, CancellationToken ct);
        Task<List<Category>> GetCategoriesAsync(CancellationToken ct);
        Task<Category> CreateCategoryAsync(Category category, CancellationToken ct);
        Task<Category> UpdateCategoryAsync(Category category, CancellationToken ct);
        Task DeleteCategoryAsync(Guid id, CancellationToken ct);
    }
}
