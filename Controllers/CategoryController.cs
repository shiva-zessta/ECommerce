using ECommerce.Dtos;
using ECommerce.Enums;
using ECommerce.Helper;
using ECommerce.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [ApiController]
    [Route("/Category")]

    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepo _catergoryRepo;

        public CategoryController(ICategoryRepo categoryRepo)
        {
            _catergoryRepo = categoryRepo;
        }

        [HttpPost]
        [Route("Create")]

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
                    Message = "Invalid login request. Please check your input.",
                    Errors = errors
                };

                return BadRequest(response);
            }
            var result = await _catergoryRepo.CreateCategory(createCategoryRequestDto.Name);
            ResponseHandler<CategoryStatus, CreateCategoryDto> resHandler = new ResponseHandler<CategoryStatus, CreateCategoryDto>();
            resHandler.Status = CategoryStatus.Success;
            resHandler.Message = "Category created successfully";
            resHandler.Data = result;
            return Ok(result);
        }

        [HttpGet]
        [Route("/all")]
        public async Task<IActionResult> GetCategoriesList()
        {
            var result = await _catergoryRepo.GetCategories();
            return Ok(result);
        }
    }
}
