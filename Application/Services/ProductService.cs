using ECommerce.Application.Dtos;
using ECommerce.Domain.Entities;
using ECommerce.Enums;
using ECommerce.Helper;
using ECommerce.Infrastructure;
using ECommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services
{
    
    public class ProductService : ServiceInterfaces.IProductService
    {
        private readonly RepositoryInterfaces.IProductRepo _productRepo;
        private readonly MyDbContext _context;
        public ProductService(RepositoryInterfaces.IProductRepo productRepo, MyDbContext context)
        {
            _context = context;
            _productRepo = productRepo;
        }

        public async Task<ResponseHandler<ProductStatus, ProductDto>> AddProduct(string productName, int categoryId, int quantity)
        {
            var productData = await _context.Product.Where(t => t.Name == productName).FirstOrDefaultAsync();
            var categoryData = await _context.Category.Where(t => t.Id == categoryId).FirstOrDefaultAsync();
            var responseHandler = new ResponseHandler<ProductStatus, ProductDto>();
            if (productData != null)
            {
                responseHandler.Data = null;
                responseHandler.Message = "Product Name already exists";
                responseHandler.Status = ProductStatus.Error;
                responseHandler.Code = 404;
                return responseHandler;
            }
            if (categoryData == null)
            {
                responseHandler.Data = null;
                responseHandler.Message = "Category Id doesn't exists";
                responseHandler.Status = ProductStatus.CategoryNotExists;
                responseHandler.Code = 404;
                return responseHandler;
            }
            if (quantity < 0)
            {
                responseHandler.Data = null;
                responseHandler.Message = "Quantity cant be negative";
                responseHandler.Status = ProductStatus.Error;
                responseHandler.Code = 400;
                return responseHandler;
            }
            var result = await _productRepo.CreateProduct(productName, categoryId, quantity);
            responseHandler.Data = result;
            responseHandler.Message = "Success";
            responseHandler.Status = ProductStatus.Success;
            responseHandler.Code = 200;
            return responseHandler;
        }

        public async Task<ResponseHandler<ProductStatus, List<ProductDto>>> GetAllProducts(int? productId)
        {
            var result = await _productRepo.GetAllProducts(productId);
            var responseHandler = new ResponseHandler<ProductStatus, List<ProductDto>>();
            if(result?.Count > 0)
            {
                responseHandler.Data = result;
                responseHandler.Message = "Success";
                responseHandler.Status = ProductStatus.Success;
                responseHandler.Code = 200;
                return responseHandler;
            }
            responseHandler.Data = result;
            responseHandler.Message = "Product not found";
            responseHandler.Status = ProductStatus.ProductNotExists;
            responseHandler.Code = 404;
            return responseHandler;
        }

        public async Task<ResponseHandler<ProductStatus, ProductDto>> UpdateProductById(int productId, int? categoryId, int? quantity, string? name)
        {
            var productData = await _context.Product.Include(c => c.Category)
                .Where(p => p.Id == productId).FirstOrDefaultAsync();
            var responseHandler = new ResponseHandler<ProductStatus, ProductDto>();

            if(productData == null)
            {
                responseHandler.Data = null;
                responseHandler.Message = "Product Id doesn't exists";
                responseHandler.Status = ProductStatus.ProductNotExists;
                responseHandler.Code = 404;
                return responseHandler;
            }
            if (categoryId.HasValue)
            {
                var categoryExists = await _context.Category.Where(c => c.Id == categoryId).FirstOrDefaultAsync();
                if(categoryExists == null)
                {
                    responseHandler.Data = null;
                    responseHandler.Message = "Category Id doesn't exists";
                    responseHandler.Status = ProductStatus.CategoryNotExists;
                    responseHandler.Code = 400;
                    return responseHandler;
                }
            }
            ;
            if (quantity != null && quantity < 0)
            {
                responseHandler.Data = null;
                responseHandler.Message = "Quantity should not be negative";
                responseHandler.Status = ProductStatus.QuantityNotValid;
                responseHandler.Code = 400;
                return responseHandler;
            }
            productData.Name = string.IsNullOrEmpty(name) ? productData.Name : name ;
            productData.CategoryId = categoryId ?? productData.CategoryId;
            productData.Quantity = quantity ?? productData.Quantity;

            var result = await _productRepo.UpdateProductById(productData);

            responseHandler.Data = result;
            responseHandler.Message = "Product data updated";
            responseHandler.Status = ProductStatus.Success;
            responseHandler.Code = 200;
            return responseHandler;
        } 

        public async Task<ResponseHandler<ProductStatus, ProductDto>> DeleteProductById(int productId)
        {
            var result = await _productRepo.DeleteProductById(productId);
            var responseHandler = new ResponseHandler<ProductStatus, ProductDto>();
            if (result == ProductStatus.Success)
            {
                responseHandler.Status = ProductStatus.Success;
                responseHandler.Message = "Product Deleted";
                responseHandler.Data = null;
                responseHandler.Code = 200;
                return responseHandler;
            }
            responseHandler.Status = ProductStatus.ProductNotExists;
            responseHandler.Message = "Product doesn't exists";
            responseHandler.Data = null;
            responseHandler.Code = 400;
            return responseHandler;

        }
    }
}
