using ECommerce.Helper;

namespace ECommerce.Application.Services
{
    public abstract class BaseService
    {
        protected ResponseHandler<TStatus, TData> BuildResponse<TStatus, TData>(
            string message,
            TStatus status,
            int code,
            TData? data = default)
        {
            return new ResponseHandler<TStatus, TData>
            {
                Data = data,
                Message = message,
                Status = status,
                Code = code
            };
        }
    }
}
