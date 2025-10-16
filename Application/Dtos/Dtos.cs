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
    public class AuthResponseDto
    {
        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
    }

    public enum UserOperationStatus
    {
        Error,
        Success,
        InvalidCredentials,
        EmailAlreadyExists,
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

    public class ProductBaseDto
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }

    }

    public class ProductDto : ProductBaseDto
    {
        public int Id { get; set; }
    }

    public class CreateProductDto : ProductBaseDto
    {
         
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
        public string? Name { get; set; } = null;
        public int? Quantity { get; set; } = null;
        public int? CategoryId { get; set; } = null;
    }

    public class AddressDto
    {
        public int Id {  get; set; }
        public int UserId { get;set; }
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public AddressType Type { get; set; } = AddressType.Shipping;
        public bool IsDefault { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
    public class CreateAddressRequestDto
    {
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public AddressType Type { get; set; } = AddressType.Shipping;
        public bool IsDefault { get; set; } = false;
    }
    public class UpdateRequestDto
    {
        [Required]
        public int Id { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? ZipCode { get; set; }
        public AddressType? Type { get; set; }
        public bool? IsDefault { get; set; }
    }

}

