namespace WorkbaseApi.Models
{
    public class ApiResponse<T>
    {
        public string Status { get; set; } = "success";
        public string Message { get; set; } = null;
        public T? Data { get; set; }

        public static ApiResponse<T> Success(T data, string message = "Success")
        {
            return new ApiResponse<T> { Status = "success", Message = message, Data = data };
        }
        public static ApiResponse<T> Error(string message = "Success")
        {
            return new ApiResponse<T> { Status = "error", Message = message, Data = default };
        }
    }
}
