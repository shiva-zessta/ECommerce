using ECommerce.Dtos;
using ECommerce.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
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
    
    }

}


