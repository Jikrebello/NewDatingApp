namespace API.DTOs
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public BaseResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }

    public class ResultResponse<T> : BaseResponse
    {
        public T Result { get; set; }

        public ResultResponse(bool success, string message, T result)
            : base(success, message)
        {
            Result = result;
        }
    }
}
