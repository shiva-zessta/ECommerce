namespace ECommerce.Helper
{
    public class ResponseHandler<TStatus, TData>
    {
        public TStatus Status { get; set; }

        public string Message { get; set; }

        public TData Data { get; set; }
        public int Code { get; set; }

    }
}



