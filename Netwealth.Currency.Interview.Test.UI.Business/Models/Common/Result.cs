using Microsoft.AspNetCore.Mvc;

namespace Netwealth.Currency.Interview.Test.UI.Business.Models.Common
{
    public class Result<T>
    {
        public T Content { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }

        public static implicit operator ActionResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
            {
                return result.Content;
            }

            return new BadRequestObjectResult(result.ErrorMessage);
        }
    }
}
