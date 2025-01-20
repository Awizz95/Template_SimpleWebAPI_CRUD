using Microsoft.EntityFrameworkCore;
using Template_SimpleWebAPI_CRUD.Data;
using Template_SimpleWebAPI_CRUD.Helpers.Exceptions;
using Template_SimpleWebAPI_CRUD.Interfaces;
using Template_SimpleWebAPI_CRUD.Models;

namespace Template_SimpleWebAPI_CRUD.Infrastructure.Services
{
    internal class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateCategoryAsync(Category category, CancellationToken ct)
        {
            if (await _context.Categories
                .AsNoTracking()
                .AnyAsync(x => x.Name == category.Name))
                    throw new DublicateException($"A category with the name {category.Name} already exists.");

            _context.Categories.Add(category);
            await _context.SaveChangesAsync(ct);

            return category;
        }

        public async Task<List<Category>> GetCategoriesAsync(CancellationToken ct)
        {
            List<Category> categories = await _context.Categories
                .AsNoTracking()
                .ToListAsync(cancellationToken: ct);

            if (categories.Count() == 0)
            {
                throw new KeyNotFoundException("There are no categories in the database. Please add a category and try again.");
            }

            return categories;
        }

        public async Task<Category> GetCategoryByIdAsync(Guid id, CancellationToken ct)
        {
            Category category = await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken: ct);

            if (category == null) 
                throw new KeyNotFoundException("The category was not found in the database.");

            return category;
        }

        public async Task<Category> GetCategoryByNameAsync(string name, CancellationToken ct)
        {
            Category category = await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == name, cancellationToken: ct);

            if (category == null) 
                throw new KeyNotFoundException("The category was not found in the database.");

            return category;
        }

        public async Task<Category> UpdateCategoryAsync(Category categoryToUpdate, CancellationToken ct)
        {
            Category category = await GetCategoryByIdAsync(categoryToUpdate.Id, ct);

            if (category.Name != categoryToUpdate.Name && await _context.Categories.AnyAsync(x => x.Name == categoryToUpdate.Name))
            {
                throw new DublicateException($"A category with the name {categoryToUpdate.Name} already exist in the database. Please choose another name.");
            }

            _context.Categories.Update(categoryToUpdate);
            await _context.SaveChangesAsync(cancellationToken: ct);

            return categoryToUpdate;
        }

        public async Task DeleteCategoryAsync(Guid id, CancellationToken ct)
        {
            if (await _context.Products
                .AnyAsync(x => x.CategoryId == id))
            {
                throw new CategoryNotEmptyException("There are products connected to this category. Please remove the products from the category and try again.");
            }

            Category category = await GetCategoryByIdAsync(id, ct);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync(cancellationToken: ct);
        }
    }

}
