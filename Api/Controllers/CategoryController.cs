using ECommerce.Application;
using ECommerce.Application.Dtos;
using ECommerce.Enums;
using ECommerce.Helper;
using ECommerce.Infrastructure;
using ECommerce.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [ApiController]
    [Route("/Category")]

    public class CategoryController : ControllerBase
    {
        private readonly RepositoryInterfaces.ICategoryRepo _catergoryRepo;
        private readonly ServiceInterfaces.ICategoryServices _categoryServices;

        public CategoryController(RepositoryInterfaces.ICategoryRepo categoryRepo, ServiceInterfaces.ICategoryServices categoryServices)
        {
            _catergoryRepo = categoryRepo;
            _categoryServices = categoryServices;
        }

        [HttpPost]
        [Route("")]

      public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDto createCategoryRequestDto)
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
            var result = await _categoryServices.AddCategory(createCategoryRequestDto.Name);
            return Ok(result);
        }

        [HttpGet]
        [Route(":id")]
        public async Task<IActionResult> GetCategoriesList(int? categoryId)
        {
            var result = await _categoryServices.GetAllCategories(categoryId);
            return Ok(result);
        }

        [HttpGet]
        [Route("productsList/:id")]
        public async Task<IActionResult> GetProductsOfCategoryById(int categoryId)
        {
            var result = await _categoryServices.GetProductsOfCategoriesById(categoryId);
            return Ok(result);
        }
    }
}
