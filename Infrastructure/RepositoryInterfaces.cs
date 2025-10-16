using ECommerce.Application.Dtos;
using ECommerce.Domain.Entities;
using ECommerce.Enums;

namespace ECommerce.Infrastructure
{
    public class RepositoryInterfaces
    {
        public interface IProductRepo
        {
            public Task<ProductDto> CreateProduct(string name, int categoryId, int quantity);
            public Task<List<ProductDto>> GetAllProducts(int? productId);
            public Task<ProductDto> UpdateProductById(Product productDto);
            public Task<ProductStatus> DeleteProductById(int productId);
        }
        public interface ICategoryRepo
        {
            public Task<Category> CreateCategory(Category category);
            public Task<List<CategoryDto>> GetCategories(int? categoryId);
            public Task<List<CategoryProductListDto>> GetProductsOfCategoryById(int categoryId);
        }
        public interface IAddressRepo
        {
            public Task<Address> AddAddress(Address addressDto);
            public Task<List<Address>> GetAddress();
            public Task<Address> UpdateAddress(Address address);
        }
    }
}
