using Microsoft.AspNetCore.Mvc;

namespace SecuritateDBAPI.Models
{
    public class ApiResponse
    {
        public bool Succes { get; set; }
        public string Message { get; set; }
        public object? Result { get; set; }

        public ApiResponse(bool succes, string message, object? result = null)
        {
            Succes = succes;
            Message = message;
            Result = result;
        }
    }
}
