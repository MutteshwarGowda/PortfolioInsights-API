using IwMetrics.Application.Enums;
using IwMetrics.Application.Models;
using IwMetricsWorks.Api.Contracts.Common;

namespace IwMetricsWorks.Api.Controllers.V1
{
    public class BaseController : ControllerBase
    {
        protected IActionResult HandleErrorResponse(List<Error> errors)
        {
            var apiError = new ErrorResponse();

            if (errors.Any(e => e.Code == ErrorCode.NotFound))
            {
                var error = errors.FirstOrDefault(e => e.Code == ErrorCode.NotFound);

                apiError.StatusCode = 404;
                apiError.Status = "Not Found";
                apiError.Timestamp = DateTime.Now;
                apiError.Errors.Add(error.Message);

                return NotFound(apiError);
            }

            apiError.StatusCode = 500;
            apiError.Status = "Bad Request";
            apiError.Timestamp = DateTime.Now;
            apiError.Errors.Add("Unknown Error");
            return StatusCode(500, apiError);
        }
    }
}
