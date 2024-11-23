using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Cms.Common.Helpers
{
    public static class ReponseHelper
    {
        public static IActionResult GetApiResponse<TEntity>(this TEntity entity, HttpContext httpContext, string message = null)
        {
            var response = ApiResponse.Create(httpContext, entity, message, true);

            return new OkObjectResult(response);
        }

        public static IActionResult GetApiResponse<TEntity>(this IEnumerable<TEntity> entity, HttpContext httpContext, string message = null)
        {
            var response = ApiResponse.Create(httpContext, entity, message, true);

            return new OkObjectResult(response);
        }

        public static IActionResult GetApiResponse(this object entity, HttpContext httpContext, string message = null)
        {
            var response = ApiResponse.Create(httpContext, entity, message, true);

            return new OkObjectResult(response);
        }

        public static async Task<IActionResult> GetApiResponseAsync(this ConfiguredTaskAwaitable configuredTaskAwaitable, HttpContext httpContext, string message = null)
        {
            await configuredTaskAwaitable;

            var response = ApiResponse.Create(httpContext, null, message, true);

            return new OkObjectResult(response);
        }
    }
}
