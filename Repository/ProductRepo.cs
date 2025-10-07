using AutoMapper;
using ECommerce.Dtos;
using ECommerce.Entities;
using ECommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repository
{
    public interface IProductRepo
    {
        public Task<ProductDto> CreateProduct(string name, int categoryId, int quantity);
        public Task<List<ProductDto>> GetAllProducts();
    }
    public class ProductRepo: IProductRepo
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;


        public ProductRepo(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ProductDto> CreateProduct(string productName, int categoryId, int quantity)
        {
            var isProductExists = await _context.Product.Where(t => t.Name == productName).FirstOrDefaultAsync();
            var isCategoryExists = await _context.Category.Where(t => t.Id == categoryId).FirstOrDefaultAsync();
            if(isProductExists == null && isCategoryExists != null && categoryId != 0)
            {
                var product = new Product
                {
                    Name = productName,
                    CategoryId = categoryId,
                    Quantity = quantity >= 0 ? quantity : throw new ArgumentException("Quantity cannot be negative")
                };

                _context.Product.Add(product);
                await _context.SaveChangesAsync();
                return new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Quantity = product.Quantity,
                    CategoryId = product.CategoryId,
                    CategoryName = isCategoryExists.Name // Map category name
                };
            }
            return null;
        }
        
        public async Task<List<ProductDto>> GetAllProducts()
        {
            var allProducts = await _context.Product
                .Include(c => c.Category)
                .ToListAsync();
            var productDto = _mapper.Map<List<ProductDto>>(allProducts);
            return productDto;
        }
    }
}
    
