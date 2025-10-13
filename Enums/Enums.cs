namespace ECommerce.Enums
{
    public enum CategoryStatus
    {
        Error = 0,
        Success = 1,
        Failed = 2,
        CategoryAlreadyExists = 3
    }

    public enum UserLoginStatus
    {
        Error = 0,
        Success = 1,
        PasswordInCorrect = 2,
        UserNotExists = 3,
    }
    public enum UserRegistrationStatus
    {
        Success = 1,
        Error = 0,
        EmailAlreadyExists = 2
    }

    public enum ProductStatus
    {
        Error = 0,
        Success = 1,
        CategoryNotExists = 2,
        QuantityNotValid = 3,
        ProductNotExists = 4
    }
}

