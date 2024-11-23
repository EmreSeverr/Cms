using Microsoft.AspNetCore.Http;

namespace Cms.Common.Helpers
{
    public class ApiResponse
    {
        public string StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }

        private ApiResponse(string statusCode, object result = null, string message = "", bool isSucces = false)
        {
            StatusCode = statusCode;
            Result = result;
            Message = message;
            IsSuccess = isSucces;
        }

        public static ApiResponse Create(HttpContext httpContext, object result = null, string message = "", bool isSucces = false)
        {
            return new ApiResponse(httpContext.Response.StatusCode.ToString(), result, message, isSucces);
        }
    }
}
