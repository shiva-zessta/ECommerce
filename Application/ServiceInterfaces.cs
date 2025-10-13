using ECommerce.Application.Dtos;
using ECommerce.Enums;
using ECommerce.Helper;

namespace ECommerce.Application
{
    public class ServiceInterfaces
    {
        public interface IProductService
        {
            public Task<ResponseHandler<ProductStatus, ProductDto>> AddProduct(string productName, int categoryId, int quantity);
            public Task<ResponseHandler<ProductStatus, List<ProductDto>>> GetAllProducts(int? productId);
            public Task<ResponseHandler<ProductStatus, ProductDto>> UpdateProductById(int productId, int? categoryId, int? quantity, string? name);
            public Task<ResponseHandler<ProductStatus, ProductDto>> DeleteProductById(int productId);
        }
    }
}
