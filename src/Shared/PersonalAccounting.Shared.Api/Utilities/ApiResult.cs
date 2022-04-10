using Microsoft.AspNetCore.Mvc;
using PersonalAccounting.Shared.Common.Utilities;

namespace PersonalAccounting.Shared.Api.Utilities
{
    public class ApiResult
    {
        public bool IsSuccess { get; set; }
        public ApiResultStatusCode ApiStatusCode { get; set; }
        public string ApiStatusCodeDescrption => ApiStatusCode.ToDisplay();
        public Dictionary<string, List<string>>? Messages { get; set; }

        public ApiResult(bool isSuccess, ApiResultStatusCode apiStatusCode)
        {
            IsSuccess = isSuccess;
            ApiStatusCode = apiStatusCode;
            Messages = new();
        }

        public ApiResult(bool isSuccess, ApiResultStatusCode apiStatusCode, string message)
        {
            IsSuccess = isSuccess;
            ApiStatusCode = apiStatusCode;
            var values = new Dictionary<string, List<string>>();
            values.Add("", new List<string>() { message });
            Messages = values;
        }

        public ApiResult(bool isSuccess, ApiResultStatusCode apiStatusCode, Dictionary<string, List<string>>? messages)
        {
            IsSuccess = isSuccess;
            ApiStatusCode = apiStatusCode;
            Messages = messages;
        }

        #region Implicit Operators
        public static implicit operator ApiResult(OkResult result)
        {
            return new ApiResult(true, ApiResultStatusCode.OK);
        }

        public static implicit operator ApiResult(BadRequestResult result)
        {
            return new ApiResult(false, ApiResultStatusCode.BadRequest);
        }

        public static implicit operator ApiResult(BadRequestObjectResult result)
        {
            var message = result.Value?.ToString();
            if (result.Value is SerializableError errors)
            {
                var messages = errors.GroupBy(x => x.Key)
                    .ToDictionary(k => k.Key, v => v.Select(x => x.Value.ToString()).ToList());
                
                return new ApiResult(false, ApiResultStatusCode.BadRequest, messages);
            }

            var values = new Dictionary<string, List<string>>();
            values.Add("", new List<string>() { message });
            return new ApiResult(false, ApiResultStatusCode.BadRequest, values);
        }

        public static implicit operator ApiResult(ContentResult result)
        {
            return new ApiResult(true, ApiResultStatusCode.OK, result.Content);
        }

        public static implicit operator ApiResult(NotFoundResult result)
        {
            return new ApiResult(false, ApiResultStatusCode.NotFound);
        }
        #endregion
    }

    public class ApiResult<TData> : ApiResult where TData : class
    {
        public TData Data { get; set; }

        public ApiResult(bool isSuccess, ApiResultStatusCode apiStatusCode, TData data)
            : base(isSuccess, apiStatusCode)
        {
            Data = data;
        }

        public ApiResult(bool isSuccess, ApiResultStatusCode apiStatusCode, TData data, string message)
            : base(isSuccess, apiStatusCode)
        {
            Data = data;
        }

        public ApiResult(bool isSuccess, ApiResultStatusCode apiStatusCode, TData data, Dictionary<string, List<string>>? messages)
             : base(isSuccess, apiStatusCode, messages)
        {
            Data = data;
        }


        #region Implicit Operators
        public static implicit operator ApiResult<TData>(TData data)
        {
            return new ApiResult<TData>(true, ApiResultStatusCode.OK, data);
        }

        public static implicit operator ApiResult<TData>(OkResult result)
        {
            return new ApiResult<TData>(true, ApiResultStatusCode.OK, null);
        }

        public static implicit operator ApiResult<TData>(OkObjectResult result)
        {
            return new ApiResult<TData>(true, ApiResultStatusCode.OK, (TData)result.Value);
        }

        public static implicit operator ApiResult<TData>(BadRequestResult result)
        {
            return new ApiResult<TData>(false, ApiResultStatusCode.BadRequest, null);
        }
        public static implicit operator ApiResult<TData>(BadRequestObjectResult result)
        {
            var message = result.Value?.ToString();
            if (result.Value is SerializableError errors)
            {
                var messages = errors.GroupBy(x => x.Key)
                    .ToDictionary(k => k.Key, v => v.Select(x => x.Value.ToString()).ToList());

                return new ApiResult<TData>(false, ApiResultStatusCode.BadRequest, null, messages);
            }

            var values = new Dictionary<string, List<string>>();
            values.Add("", new List<string>() { message });
            return new ApiResult<TData>(false, ApiResultStatusCode.BadRequest, null, values);
        }

        public static implicit operator ApiResult<TData>(ContentResult result)
        {
            return new ApiResult<TData>(true, ApiResultStatusCode.OK, null, result.Content);
        }

        public static implicit operator ApiResult<TData>(NotFoundResult result)
        {
            return new ApiResult<TData>(false, ApiResultStatusCode.NotFound, null);
        }

        public static implicit operator ApiResult<TData>(NotFoundObjectResult result)
        {
            return new ApiResult<TData>(false, ApiResultStatusCode.NotFound, (TData)result.Value);
        }
        #endregion
    }
}