using System.ComponentModel.DataAnnotations;

namespace PersonalAccounting.Shared.Common.Utilities
{
    public enum ApiResultStatusCode
    {
        [Display(Name = "عملیات با موفقیت انجام شد")]
        OK = 200,

        [Display(Name = "پارامترهای ارسالی معتبر نیستند")]
        BadRequest = 400,

        [Display(Name = "خطای احراز هویت")]
        Unauthorized = 401,

        [Display(Name = "اطلاعاتی با مشخصات ارسالی یافت نشد")]
        NotFound = 404,

        [Display(Name = "خطایی در سرور رخ داده است")]
        InternalServerError = 500,
    }
}