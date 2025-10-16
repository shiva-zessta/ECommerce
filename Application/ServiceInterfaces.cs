using ECommerce.Application.Dtos;
using ECommerce.Enums;
using ECommerce.Helper;
using System.Threading.Tasks;

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

        public interface ICategoryServices
        {
            public Task<ResponseHandler<CategoryStatus, CategoryDto>> AddCategory(string categoryName);
            public Task<ResponseHandler<CategoryStatus, List<CategoryDto>>> GetAllCategories(int? categoryId);
            public Task<ResponseHandler<CategoryStatus, List<CategoryProductListDto>>> GetProductsOfCategoriesById(int categoryId);

        }

        public interface IAddressServices
        {
            public Task<ResponseHandler<AddressStatus, AddressDto>> AddAddress(CreateAddressRequestDto createAddressRequestDto);
            public Task<ResponseHandler<AddressStatus, List<AddressDto>>> GetAddress();
            public Task<ResponseHandler<AddressStatus, AddressDto>> UpdateAddress(UpdateRequestDto updateRequestDto);
        }

        public interface IUserInfo
        {
            int UserId { get; }
            string Email { get; }
        }

    }
}
