using AutoMapper;
using DTO.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template_SimpleWebAPI_CRUD.Interfaces;
using Template_SimpleWebAPI_CRUD.Models;
using Template_SimpleWebAPI_CRUD.Models.DTO.Categories;

namespace Template_SimpleWebAPI_CRUD.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;

        public CategoryController(IMapper mapper, ICategoryService categoryService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAllCategories(CancellationToken ct)
        {
            var categories = await _categoryService.GetCategoriesAsync(ct);

            return Ok(categories);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id, CancellationToken ct)
        {
            var category = await GetCategoryById(id, ct);

            return Ok(category);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryAddRequestDto categoryToAdd, CancellationToken ct)
        {
            Category mappedCategoryToAdd = _mapper.Map<Category>(categoryToAdd);

            Category addedCategory = await _categoryService.CreateCategoryAsync(mappedCategoryToAdd, ct);

            CategoryResponseDto mappedCategoryToReturn = _mapper.Map<CategoryResponseDto>(addedCategory);

            return Ok(mappedCategoryToReturn);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryUpdateRequestDto categoryToUpdate, CancellationToken ct)
        {
            Category mappedCategoryToUpdate = _mapper.Map<Category>(categoryToUpdate);

            Category updatedCategory = await _categoryService.UpdateCategoryAsync(mappedCategoryToUpdate, ct);

            CategoryResponseDto mappedCategoryToReturn = _mapper.Map<CategoryResponseDto>(updatedCategory);

            return Ok(mappedCategoryToReturn);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id, CancellationToken ct)
        {
            await _categoryService.DeleteCategoryAsync(id, ct);

            return Ok("Category deleted successfully");
        }
    }
}
