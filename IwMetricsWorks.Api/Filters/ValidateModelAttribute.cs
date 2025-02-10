using IwMetricsWorks.Api.Contracts.Common;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IwMetricsWorks.Api.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public virtual void OnResultExecuting(ResultExecutingContext context) 
        {
            if (!context.ModelState.IsValid)
            {
                var apiError = new ErrorResponse();
                apiError.StatusCode = 400;
                apiError.Status = "Bad Request";
                apiError.Timestamp = DateTime.Now;

                var errors = context.ModelState.AsEnumerable();

                foreach (var error in errors)
                {
                    foreach (var inner in error.Value.Errors)
                    {
                        apiError.Errors.Add(inner.ErrorMessage);
                    }
                }

                context.Result = new BadRequestObjectResult(apiError);
            }
        }
    }
}
