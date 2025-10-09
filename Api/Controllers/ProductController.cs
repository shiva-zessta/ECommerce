using ECommerce.Application.Dtos;
using ECommerce.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace ECommerce.Api.Controllers
{
    [ApiController]
    [Route("/Product")]
    public class ProductController: ControllerBase
    {
        private readonly IProductRepo _repo;
        public ProductController(IProductRepo repo) { _repo = repo; }

        [HttpPost]
        [Route("Add")]

        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequestDto createProductRequestDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var response = new
                {
                    Status = 400,
                    Message = "Invalid request. Please check your input.",
                    Errors = errors
                };

                return BadRequest(response);
            }

            var result = await _repo.CreateProduct(createProductRequestDto.Name, createProductRequestDto.CategoryId,createProductRequestDto.Quantity);
            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _repo.GetAllProducts();
            return Ok(result);
        }

        [HttpGet]
        [Route(":Id")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var result = await _repo.GetProductById(productId);
            var response = new
            {
                Data = result,
            };
            return Ok(response);
        }

        [HttpPut]
        [Route(":Id")]
        public async Task<IActionResult> UpdateProductById([FromBody] UpdateProductRequestDto updateProductRequestDto)
        {
            var result = await _repo.UpdateProductById(updateProductRequestDto.ProductId, updateProductRequestDto.Name, updateProductRequestDto.CategoryId, updateProductRequestDto.Quantity);
            return Ok(result);
        }

        [HttpDelete]
        [Route(":Id")]
        public async Task<IActionResult> DeleteProductById([FromQuery] int productId)
        {
            var result = await _repo.DeleteProductById(productId);
            return Ok(result);
        }

    }

}


