using AutoMapper;
using ECommerce.Domain.Entities;
using ECommerce.Enums;
using ECommerce.Helper;
using Microsoft.EntityFrameworkCore;
using ECommerce.Application.Dtos;

namespace ECommerce.Infrastructure.Repository
{
    public interface IProductRepo
    {
        public Task<ProductDto> CreateProduct(string name, int categoryId, int quantity);
        public Task<List<ProductDto>> GetAllProducts();
        public Task<ProductDto> GetProductById(int productId);
        public Task<ResponseHandler<UpdateProductStatus, ProductDto>> UpdateProductById(int productId, string name, int? categoryId, int? quantity);
        public Task<ResponseHandler<UpdateProductStatus, ProductDto>> DeleteProductById(int productId);
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

        public async Task<ProductDto> GetProductById(int productId)
        {
            var productData = await _context.Product
               .Include(c => c.Category)
               .Where(p => p.Id == productId)
               .FirstOrDefaultAsync();
            if(productData == null)
            {
                return null;
            }
            var productMapped = _mapper.Map<ProductDto>(productData);
            return productMapped;
        }

        public async Task<ResponseHandler<UpdateProductStatus, ProductDto>> UpdateProductById(int productId, string name, int? categoryId, int? quantity)
        {
            var productData = await _context.Product.Include(c => c.Category)
                .Where(p => p.Id == productId).FirstOrDefaultAsync();
            var categoryExists = await _context.Category.Where(c => c.Id == categoryId).FirstOrDefaultAsync();
            var responseHandler = new ResponseHandler<UpdateProductStatus, ProductDto>();
            if (productData == null)
            {
                responseHandler.Data = null;
                responseHandler.Message = "Product Id doesn't exists";
                responseHandler.Status = UpdateProductStatus.ProductNotExists;
                responseHandler.Code = 404;
                return responseHandler;
            }
            if (categoryExists == null)
            {
                responseHandler.Data = null;
                responseHandler.Message = "Category Id doesn't exists";
                responseHandler.Status = UpdateProductStatus.CategoryNotExists;
                responseHandler.Code = 400;
                return responseHandler;
            };
            if (quantity != null && quantity < 0)
            {
                responseHandler.Data = null;
                responseHandler.Message = "Quantity should not be negative";
                responseHandler.Status = UpdateProductStatus.QuantityNotValid;
                responseHandler.Code = 400;
                return responseHandler;
            }
            productData.Name = name ?? productData.Name;
            productData.CategoryId = categoryId ?? productData.CategoryId;
            productData.Quantity = quantity ?? productData.Quantity;



            _context.Product.Update(productData);
            await _context.SaveChangesAsync();
            var productMapped = _mapper.Map<ProductDto>(productData);
            responseHandler.Data = productMapped;
            responseHandler.Message = "Product data updated";
            responseHandler.Status = UpdateProductStatus.Success;
            responseHandler.Code = 200;
            return responseHandler;
        }

        public async Task<ResponseHandler<UpdateProductStatus, ProductDto>> DeleteProductById(int productId)
        {
            var productData = await _context.Product.Where(p => p.Id == productId).FirstOrDefaultAsync();
            var responseHandler = new ResponseHandler<UpdateProductStatus, ProductDto>();
            if (productData == null)
            {
                responseHandler.Status = UpdateProductStatus.ProductNotExists;
                responseHandler.Message = "Product doesn't exists";
                responseHandler.Data = null;
                responseHandler.Code = 400;
                return responseHandler;
            }
            _context.Product.Remove(productData);
            await _context.SaveChangesAsync();
            responseHandler.Status = UpdateProductStatus.Success;
            responseHandler.Message = "Product Deleted";
            responseHandler.Data = null;
            responseHandler.Code = 200;
            return responseHandler;
        }
    }
}
    
