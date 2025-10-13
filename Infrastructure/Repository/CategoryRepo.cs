using AutoMapper;
using ECommerce.Application.Dtos;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Repository
{

    public interface ICategoryRepo
    {
        public Task<CreateCategoryDto> CreateCategory(string name);
        public Task<List<CategoryDto>> GetCategories();
        public Task<CategoryDto> GetCategoryByCategoryId(int categoryId);

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
                categoryDto.Id = category.Id;
                categoryDto.Name = name;
                createCategoryDto.Category = categoryDto;
                createCategoryDto.Status = Enums.CategoryStatus.Success;
                return createCategoryDto;
            }
            createCategoryDto.Category = null;
            createCategoryDto.Status = Enums.CategoryStatus.CategoryAlreadyExists;
        return createCategoryDto;
           }

        public async Task<List<CategoryDto>> GetCategories()
        {
            var getCategoriesList = await _context.Category.Include(c => c.Products).ToListAsync();
            var allCategories =  _mapper.Map<List<CategoryDto>>(getCategoriesList);
            return allCategories;
        }

        public async Task<CategoryDto> GetCategoryByCategoryId(int categoryId)
        {
            var categoryById = await _context.Category.Include(c => c.Products).Where(t => t.Id == categoryId).FirstOrDefaultAsync();
            if(categoryById == null)
            {
                return null;
            }
            var categoryMapped = _mapper.Map<CategoryDto>(categoryById);
            return categoryMapped;
        }
    }
}
 