using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PersonalAccounting.Shared.Api.Utilities;
using PersonalAccounting.Shared.Common.Utilities;

namespace PersonalAccounting.Shared.Api.Filters
{
    public class ApiResultFilterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is OkObjectResult okObjectResult)
            {
                var apiResult = new ApiResult<object>(true, ApiResultStatusCode.OK, okObjectResult.Value);
                context.Result = new JsonResult(apiResult) { StatusCode = okObjectResult.StatusCode };
            }
            else if (context.Result is OkResult okResult)
            {
                var apiResult = new ApiResult(true, ApiResultStatusCode.OK);
                context.Result = new JsonResult(apiResult) { StatusCode = okResult.StatusCode };
            }
            else if (context.Result is ObjectResult badRequestObjectResult && badRequestObjectResult.StatusCode == 400)
            {
                Dictionary<string, List<string>> messages = new Dictionary<string, List<string>>();
                IEnumerable<string> errorMessages;
                switch (badRequestObjectResult.Value)
                {
                    case ValidationProblemDetails validationProblemDetails:
                        messages = validationProblemDetails.Errors.GroupBy(x => x.Key)
                            .ToDictionary(k => k.Key, v => v.Select(x => x.Value.ToString()).ToList());
                        break;

                    case SerializableError errors:
                        messages = errors.GroupBy(x => x.Key)
                            .ToDictionary(k => k.Key, v => v.Select(x => x.Value.ToString()).ToList());
                        break;

                    case var value when value != null && !(value is ProblemDetails):
                        var values = new Dictionary<string, List<string>>();
                        values.Add("", new List<string>() { badRequestObjectResult.Value.ToString() });
                        break;
                }

                var apiResult = new ApiResult(false, ApiResultStatusCode.BadRequest, messages);
                context.Result = new JsonResult(apiResult) { StatusCode = badRequestObjectResult.StatusCode };
            }
            else if (context.Result is ObjectResult notFoundObjectResult && notFoundObjectResult.StatusCode == 404)
            {
                string message = null;
                if (notFoundObjectResult.Value != null && !(notFoundObjectResult.Value is ProblemDetails))
                    message = notFoundObjectResult.Value.ToString();

                var apiResult = new ApiResult(false, ApiResultStatusCode.NotFound, message);
                context.Result = new JsonResult(apiResult) { StatusCode = notFoundObjectResult.StatusCode };
            }
            else if (context.Result is ContentResult contentResult)
            {
                var apiResult = new ApiResult(true, ApiResultStatusCode.OK, contentResult.Content);
                context.Result = new JsonResult(apiResult) { StatusCode = contentResult.StatusCode };
            }
            else if (context.Result is ObjectResult objectResult && objectResult.StatusCode == null
                && !(objectResult.Value is ApiResult))
            {
                var apiResult = new ApiResult<object>(true, ApiResultStatusCode.OK, objectResult.Value);
                context.Result = new JsonResult(apiResult) { StatusCode = objectResult.StatusCode };
            }

            base.OnResultExecuting(context);
        }
    }
}
