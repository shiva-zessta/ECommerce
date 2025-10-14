using AutoMapper;
using ECommerce.Application.Dtos;
using ECommerce.Domain.Entities;
using ECommerce.Enums;
using ECommerce.Helper;
using ECommerce.Infrastructure;
using ECommerce.Infrastructure.Persistence;
using ECommerce.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using static ECommerce.Application.ServiceInterfaces;

namespace ECommerce.Application.Services
{
    public class CategoryService : ICategoryServices
    {
        private readonly RepositoryInterfaces.ICategoryRepo _categoryRepo;
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(RepositoryInterfaces.ICategoryRepo repo, MyDbContext context, IMapper mapper)
        {
            _categoryRepo = repo;
            _context = context;
            _mapper = mapper;
        }
        public async Task<ResponseHandler<CategoryStatus, CategoryDto>> AddCategory(string categoryName)
        {
            var category = await _context.Category.Where(t => t.Name == categoryName).FirstOrDefaultAsync();
            var responseHandler = new ResponseHandler<CategoryStatus, CategoryDto>();

            if (category == null)
            {
                var categoryData = new Category
                {
                    Name = categoryName,
                };
                var result = await _categoryRepo.CreateCategory(categoryData);
                var mappedCategoryDto= _mapper.Map<CategoryDto>(result); 
                responseHandler.Data = mappedCategoryDto;
                responseHandler.Message = "Success";
                responseHandler.Status = CategoryStatus.Success;
                responseHandler.Code = 200;
                return responseHandler;
            }
            responseHandler.Data = null;
            responseHandler.Message = "Caegory already exists";
            responseHandler.Status = CategoryStatus.CategoryAlreadyExists;
            responseHandler.Code = 404;
            return responseHandler;

        }
        public async Task<ResponseHandler<CategoryStatus, List<CategoryDto>>> GetAllCategories(int? categoryId)
        {
            var result = await _categoryRepo.GetCategories(categoryId);
            var responseHandler = new ResponseHandler<CategoryStatus, List<CategoryDto>>();
            if (result?.Count > 0)
            {
                responseHandler.Data = result;
                responseHandler.Message = "Success";
                responseHandler.Status = CategoryStatus.Success;
                responseHandler.Code = 200;
                return responseHandler;
            }
            responseHandler.Data = result;
            responseHandler.Message = "Product not found";
            responseHandler.Status = CategoryStatus.Error;
            responseHandler.Code = 404;
            return responseHandler;
        }

        public async Task<ResponseHandler<CategoryStatus, List<CategoryProductListDto>>> GetProductsOfCategoriesById (int categoryId)
        {
            var responseHandler = new ResponseHandler<CategoryStatus, List<CategoryProductListDto>>();
            
            if (categoryId < 1)
            {
                responseHandler.Data = null;
                responseHandler.Message = "Category Id cant be negative";
                responseHandler.Status = CategoryStatus.Error;
                responseHandler.Code = 404;
                return responseHandler;
            }
            var result = await _categoryRepo.GetProductsOfCategoryById(categoryId);
            responseHandler.Data = result;
            responseHandler.Message = "Success";
            responseHandler.Status = CategoryStatus.Success;
            responseHandler.Code = 200;
            return responseHandler;
        }

    }
}
