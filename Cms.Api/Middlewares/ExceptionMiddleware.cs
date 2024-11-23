using Cms.Common.Exceptions;
using Cms.Common.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Cms.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext httpContext)
        {
            string errorMessage = string.Empty;
            try
            {
                await _next.Invoke(httpContext).ConfigureAwait(false);

                if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    string message = "Page not founddd.";
                    var response = ApiResponse.Create(httpContext, message: message, isSucces: false);

                    httpContext.Response.StatusCode = StatusCodes.Status200OK;
                    httpContext.Response.ContentType = "application/json; charset=UTF-8";
                    await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response)).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                if (ex is CmsApiException)
                {
                    errorMessage = ex.Message;
                }
                else
                {
                    errorMessage = "Sistemsel bir sıkıntıyla karşılaşılmıştır.";
                }

                var response = ApiResponse.Create(httpContext, null, errorMessage, false);

                DefaultContractResolver contractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };

                httpContext.Response.StatusCode = StatusCodes.Status200OK;
                httpContext.Response.ContentType = "application/json; charset=UTF-8";
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings()
                {
                    ContractResolver = contractResolver,
                    Formatting = Newtonsoft.Json.Formatting.Indented
                })).ConfigureAwait(false);
            }
        }
    }
}
