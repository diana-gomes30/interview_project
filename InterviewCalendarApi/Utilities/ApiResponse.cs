namespace InterviewCalendarApi.Utilities
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string? message = null, object? result = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageStatusCode(statusCode);
            Data = result!;
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        private string GetDefaultMessageStatusCode(int statusCode)
        {
            return statusCode switch
            {
                200 => "Ok",
                201 => "Created",
                400 => "Bad Request",
                401 => "Unauthorized",
                403 => "Forbidden",
                404 => "Not Found",
                _ => throw new NotImplementedException()
            };
        }
    }
}
