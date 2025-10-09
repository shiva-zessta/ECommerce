using ECommerce.Domain.Entities;
using ECommerce.Enums;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.Dtos
{
    public class PasswordHashResultDto
    {
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
    }

    public class UserLoginRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UserRegisterDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
    }

    public class UserLoginDto
    {
        public UserDto UserData { get; set; }
        public UserLoginStatus Status { get; set; }
    }

    public class CreateCategoryRequestDto
    {
        public string Name { get; set; }
    }

    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CategoryProductListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CategoryProductDto> Products { get; set; }
    }

    public class CreateCategoryDto
    {
        public CategoryDto Category { get; set; }

        public CategoryStatus Status { get; set; }
    }

    public class CreateProductRequestDto
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int Quantity { get; set; }
    }

    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

    public class CategoryProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdateProductRequestDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
    }
}

