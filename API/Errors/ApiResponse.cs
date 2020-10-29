using System;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int status, string message = null)
        {
            Status = status;
            Message = message ?? GetDefaultMessageForStatusCode(status);
        }

        public int Status { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessageForStatusCode(int status)
        {
            return status switch{
                400 => "Bad request",
                401 => "Not authorized",
                404 => "Resource not found",
                500 => "Internal server error",
                _ => "an error occured"
            };
        }
    }
}