using AutoMapper;
using ECommerce.Application.Dtos;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Repository
{
    public class CategoryRepo : RepositoryInterfaces.ICategoryRepo
    {
        private readonly MyDbContext _context;

        private readonly IMapper _mapper;

        public CategoryRepo(MyDbContext myDbContext, IMapper mapper)
        {
            _context = myDbContext;
            _mapper = mapper;
        } 

        public async Task<Category> CreateCategory(Category category)
        {
                _context.Category.Add(category);
                _context.SaveChanges();
                return category;
        }
     

        public async Task<List<CategoryDto>> GetCategories(int? categoryId)
        {
            var getCategoriesList = categoryId != null ?
                await _context.Category.
                Where(c => c.Id == categoryId).
                ToListAsync() : 
                await _context.Category.
                ToListAsync();
            var allCategories =  _mapper.Map<List<CategoryDto>>(getCategoriesList);
            return allCategories;
        }
        public async Task<List<CategoryProductListDto>> GetProductsOfCategoryById(int categoryId)
        {
            var getCategoriesList = await _context.Category.
                Where(c => c.Id == categoryId).
                Include(p => p.Products).
                ToListAsync();
            var allCategories = _mapper.Map<List<CategoryProductListDto>>(getCategoriesList);
            return allCategories;
        }
    }
}
 