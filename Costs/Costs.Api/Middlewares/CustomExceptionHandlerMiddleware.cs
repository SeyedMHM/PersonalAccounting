using Costs.Api.Utilities;
using PersonalAccounting.Shared.Common.Utilities;

namespace Costs.Api.Middlewares
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
            //try
            //{
            //    await _next(context);
            //}
            //catch (FluentValidation.ValidationException exception)
            //{
            //    FluentResults.Result result = new FluentResults.Result();

            //    int httpStatusCode = HttpStatusCode.BadRequest.ToValue();
            //    context.Response.StatusCode = httpStatusCode;
            //    context.Response.ContentType = "application/json";

            //    foreach (var error in exception.Errors)
            //    {
            //        result.WithError(error.ErrorMessage);
            //    }

            //    var option = new JsonSerializerOptions
            //    {
            //        IncludeFields = true,
            //        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            //    };
            //    var resultString = JsonSerializer.Serialize(result, option);
            //    context.Response.WriteAsync(resultString);
            //}
            //catch (Exception exception)
            //{
            //    //log error
            //    FluentResults.Result result = new FluentResults.Result();

            //    int httpStatusCode = HttpStatusCode.InternalServerError.ToValue();
            //    context.Response.StatusCode = httpStatusCode;
            //    context.Response.ContentType = "application/json";

            //    result.WithError(exception.Message);

            //    var option = new JsonSerializerOptions
            //    {
            //        IncludeFields = true,
            //        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            //    };
            //    var resultString = JsonSerializer.Serialize(result, option);
            //    context.Response.WriteAsync(resultString);
            //}

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