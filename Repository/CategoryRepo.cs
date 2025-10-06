using AutoMapper;
using ECommerce.Dtos;
using ECommerce.Entities;
using ECommerce.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ECommerce.Repository
{

    public interface ICategoryRepo
    {
        public Task<CreateCategoryDto> CreateCategory(string name);
        public Task<List<Category>> GetCategories();

    }
    public class CategoryRepo : ICategoryRepo
    {
        private readonly MyDbContext _context;

        private readonly IMapper _mapper;

        public CategoryRepo(MyDbContext myDbContext, IMapper mapper)
        {
            _context = myDbContext;
            _mapper = mapper;
        }

        public async Task<CreateCategoryDto> CreateCategory(string name)
        {
            var isExisitingCategory = await _context.Category.Where(t => t.Name == name).FirstOrDefaultAsync();
            CreateCategoryDto createCategoryDto = new CreateCategoryDto();
            CategoryDto categoryDto = new CategoryDto();
            if (isExisitingCategory == null)
            {
                Category category = new Category();
                category.Name = name;
                _context.Category.Add(category);
                _context.SaveChanges();
                categoryDto.Name = name;
                createCategoryDto.Category = categoryDto;
                createCategoryDto.Status = Enums.CategoryStatus.Success;
                return createCategoryDto;
            }
            createCategoryDto.Category = null;
            createCategoryDto.Status = Enums.CategoryStatus.CategoryAlreadyExists;
        return createCategoryDto;
           }

        public async Task<List<Category>> GetCategories()
        {
            var getCategoriesList = await _context.Category.ToListAsync();
           return getCategoriesList;
        }
    }
}
