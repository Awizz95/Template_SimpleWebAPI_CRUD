using Microsoft.EntityFrameworkCore;
using Template_SimpleWebAPI_CRUD.Data;
using Template_SimpleWebAPI_CRUD.Helpers.Exceptions;
using Template_SimpleWebAPI_CRUD.Interfaces;
using Template_SimpleWebAPI_CRUD.Models;

namespace Template_SimpleWebAPI_CRUD.Infrastructure.Services
{
    internal class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly ICategoryService _categories;

        public ProductService(AppDbContext context, ICategoryService categories)
        {
            _context = context;
            _categories = categories;
        }

        public async Task<Product> CreateProductAsync(Product product, CancellationToken ct)
        {
            if (await _context.Products.AsNoTracking().AnyAsync(x => x.Name == product.Name))
                throw new DublicateException($"A product with the name {product.Name} already exists.");

            //убеждаемся, что категория существует
            await _categories.GetCategoryByIdAsync(product.CategoryId, ct);

            _context.Products.Add(product);
            await _context.SaveChangesAsync(ct);

            return product;
        }

        public async Task<Product> GetProductByIdAsync(Guid productId, CancellationToken ct)
        {
            Product product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == productId, ct);

            if (product == null) 
                throw new KeyNotFoundException($"The product with ID: {productId} was not found in the database.");

            return product;
        }

        public async Task<Product> GetProductByNameAsync(string name, CancellationToken ct)
        {
            Product product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Name == name, ct);

            if (product == null)
                throw new KeyNotFoundException($"The product with name: {name} was not found in the database.");

            return product;
        }

        public async Task<List<Product>> GetProductsAsync(CancellationToken ct)
        {
            List<Product> products = await _context.Products
                .AsNoTracking()
                .ToListAsync(ct);

            if (products.Count() == 0)
            {
                throw new KeyNotFoundException("There are no products in the database. Please add one or more products and try again.");
            }

            return products;
        }

        public async Task<List<Product>> GetProductsByCategoryIdAsync(Guid categoryId, CancellationToken ct)
        {
            Category category = await _categories.GetCategoryByIdAsync(categoryId, ct);

            List<Product> products = await _context.Products
                .AsNoTracking()
                .Where(x => x.CategoryId == categoryId)
                .ToListAsync(ct);

            if (products.Count() == 0)
            {
                throw new KeyNotFoundException($"The category {category.Name} contains 0 products. Add one or more products to the category and try again.");
            }

            return products;
        }

        public async Task<Product> UpdateProductAsync(Product productToUpdate, CancellationToken ct)
        {
            Product product = await GetProductByIdAsync(productToUpdate.Id, ct);

            if (product.Name != productToUpdate.Name && await _context.Products.AnyAsync(x => x.Name == productToUpdate.Name))
            {
                throw new DublicateException($"A product with the name {productToUpdate.Name} already exist in the database. Please choose another name for the product and try again.");
            }

            _context.Products.Update(productToUpdate);
            await _context.SaveChangesAsync(ct);

            return productToUpdate;
        }

        public async Task DeleteProductAsync(Guid productId, CancellationToken ct)
        {
            Product product = await GetProductByIdAsync(productId, ct);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync(ct);
        }
    }
}
