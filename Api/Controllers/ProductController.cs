using ECommerce.Application;
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
        private readonly ServiceInterfaces.IProductService _services;
        public ProductController(ServiceInterfaces.IProductService services) { _services = services; }

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

            var result = await _services.AddProduct(createProductRequestDto.Name, createProductRequestDto.CategoryId,createProductRequestDto.Quantity);
            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route(":id")]
        public async Task<IActionResult> GetAllProducts([FromQuery] int? productId)
        {
            var result = await _services.GetAllProducts(productId);
            return Ok(result);
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateProductById([FromBody] UpdateProductRequestDto updateProductRequestDto)
        {
            var result = await _services.UpdateProductById(updateProductRequestDto.ProductId,  updateProductRequestDto.CategoryId, updateProductRequestDto.Quantity, updateProductRequestDto.Name);
            return Ok(result);
        }

        [HttpDelete]
        [Route(":Id")]
        public async Task<IActionResult> DeleteProductById([FromQuery] int productId)
        {
            var result = await _services.DeleteProductById(productId);
            return Ok(result);
        }

    }

}


