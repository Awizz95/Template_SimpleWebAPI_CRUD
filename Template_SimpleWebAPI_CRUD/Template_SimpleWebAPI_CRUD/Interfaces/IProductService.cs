using Template_SimpleWebAPI_CRUD.Models;

namespace Template_SimpleWebAPI_CRUD.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(Guid productId, CancellationToken ct);
        Task<List<Product>> GetProductsAsync(CancellationToken ct);
        Task<List<Product>> GetProductsByCategoryIdAsync(Guid categoryId, CancellationToken ct);
        Task<Product> CreateProductAsync(Product product, CancellationToken ct);
        Task<Product> UpdateProductAsync(Product product, CancellationToken ct);
        Task DeleteProductAsync(Guid productId, CancellationToken ct);
    }
}
