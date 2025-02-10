using IwMetricsWorks.Api.Contracts.Common;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IwMetricsWorks.Api.Filters
{
    public class IwMetricsExceptionHandler : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var apiError = new ErrorResponse();
            apiError.StatusCode = 500;
            apiError.Status = "Internal Server Error";
            apiError.Timestamp = DateTime.Now;
            apiError.Errors.Add(context.Exception.Message);

            context.Result = new JsonResult(apiError) { StatusCode = 500 };
        }
    }
}
