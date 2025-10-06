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
}

