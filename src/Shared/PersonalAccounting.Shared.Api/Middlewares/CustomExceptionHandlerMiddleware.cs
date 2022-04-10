using PersonalAccounting.Shared.Api.Utilities;
using PersonalAccounting.Shared.Common.Utilities;

namespace PersonalAccounting.Shared.Api.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (FluentValidation.ValidationException exception)
            {
                var errors = exception.Errors
                     .GroupBy(x => x.PropertyName)
                     .ToDictionary(k => k.Key, v => v.Select(x => x.ErrorMessage)
                     .ToList());

                ApiResultStatusCode statusCode = ApiResultStatusCode.BadRequest;
                ApiResult apiResult = new ApiResult(false, statusCode, errors);

                context.Response.StatusCode = statusCode.ToValue();
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(apiResult);
            }
            catch (Exception exception)
            {
                ApiResultStatusCode statusCode = ApiResultStatusCode.InternalServerError;
                ApiResult apiResult = new ApiResult(false, statusCode, exception.Message);

                context.Response.StatusCode = statusCode.ToValue();
                context.Response.ContentType = "application/json";

               await  context.Response.WriteAsJsonAsync(apiResult);
            }
        }
    }
}