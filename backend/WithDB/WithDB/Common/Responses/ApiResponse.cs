namespace WithDB.Common.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; init; }
        public string Message { get; init; }
        public T Data { get; init; }

        private ApiResponse(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public static ApiResponse<T> Ok(T data, string message = "OK")
            => new ApiResponse<T>(true, message, data);

        public static ApiResponse<T> Fail(string message)
            => new ApiResponse<T>(false, message, default);
    }
}
