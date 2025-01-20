using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Template_SimpleWebAPI_CRUD.Interfaces;
using Template_SimpleWebAPI_CRUD.Models;
using Template_SimpleWebAPI_CRUD.Models.DTO.Products;

namespace API.Controllers.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductController(IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _productService = productService;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAllProductsAsync(CancellationToken ct)
        {
            List<Product> products = await _productService.GetProductsAsync(ct);

            List<ProductResponseDto> mappedProducts = _mapper.Map<List<ProductResponseDto>>(products);

            return Ok(mappedProducts);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetProductByIdAsync(Guid id, CancellationToken ct)
        {
            Product product = await _productService.GetProductByIdAsync(id, ct);

            ProductResponseDto mappedProduct = _mapper.Map<ProductResponseDto>(product);

            return Ok(mappedProduct);
        }

        [HttpGet("get/category/{id}")]
        public async Task<IActionResult> GetProductByCategoryAsync(Guid id, CancellationToken ct)
        {
            List<Product> products = await _productService.GetProductsByCategoryIdAsync(id, ct);

            List<ProductResponseDto> mappedProductsInCategory = _mapper.Map<List<ProductResponseDto>>(products);

            return Ok(mappedProductsInCategory);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddProductAsync(ProductCreateRequestDto productToCreate, CancellationToken ct)
        {
            Product product = _mapper.Map<Product>(productToCreate);

            product = await _productService.CreateProductAsync(product, ct);

            ProductResponseDto mappedProduct = _mapper.Map<ProductResponseDto>(product);

            return Ok(mappedProduct);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateProductAsync(ProductUpdateRequestDto productToUpdate, CancellationToken ct)
        {
            Product product = _mapper.Map<Product>(productToUpdate);

            product = await _productService.UpdateProductAsync(product, ct);

            ProductResponseDto mappedProduct = _mapper.Map<ProductResponseDto>(product);

            return Ok(mappedProduct);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProductAsync(Guid id, CancellationToken ct)
        {
            await _productService.DeleteProductAsync(id, ct);

            return Ok("Product has successfully been removed from the database.");
        }
    }
}
