using AutoMapper;
using ECommerce.Domain.Entities;
using ECommerce.Enums;
using ECommerce.Helper;
using Microsoft.EntityFrameworkCore;
using ECommerce.Application.Dtos;
using ECommerce.Infrastructure.Persistence;

namespace ECommerce.Infrastructure.Repository
{
    public class ProductRepo: RepositoryInterfaces.IProductRepo
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
            
                var product = new Product
                {
                    Name = productName,
                    CategoryId = categoryId,
                    Quantity = quantity
                };

                _context.Product.Add(product);
                await _context.SaveChangesAsync();
                return new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Quantity = product.Quantity,
                    CategoryId = product.CategoryId,
                };
        }
        
        public async Task<List<ProductDto>> GetAllProducts(int? productId)
        {
            var allProducts = productId != null ? await _context.Product
                .Include(c => c.Category)
                .Where(p => p.Id == productId)
                .ToListAsync()  : await _context.Product
                .Include(c => c.Category)
                .ToListAsync();
            var productDto = _mapper.Map<List<ProductDto>>(allProducts);
            return productDto;
        }
        
        public async Task<ProductDto> UpdateProductById(Product productData)
        {
            var responseHandler = new ResponseHandler<ProductStatus, ProductDto>();
            _context.Product.Update(productData);
            await _context.SaveChangesAsync();
            var productMapped = _mapper.Map<ProductDto>(productData);
            return productMapped;
        }

       public async Task<ProductStatus> DeleteProductById(int productId)
        {
            var productData = await _context.Product.Where(p => p.Id == productId).FirstOrDefaultAsync();
            if(productData == null)
            {
                return ProductStatus.ProductNotExists;
            }
            _context.Product.Remove(productData);
            await _context.SaveChangesAsync();
            return ProductStatus.Success;
        }
    }
}
    
