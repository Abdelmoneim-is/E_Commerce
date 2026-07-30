using E_Commerce.Application.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIBaseController : ControllerBase
    {
        public static ActionResult<T> ToActionResult<T> (Result<T> result)
        {
            if(result.IsSucess)
            {
                return new OkObjectResult (result.data);
            }
            else
            {
                return ToProblem(result.Errors);
            }
        }

        public static ActionResult ToActionResult (Result result)
        {
            if (result.IsSucess)
            {
                return new OkResult();
            }
            else
            {
                return ToProblem(result.Errors);
            }
        }

        public static ObjectResult ToProblem (IReadOnlyList<Error> errors)
        {
            var firstError = errors[0];
            var statusCode = firstError.errorType switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Unauhorized => StatusCodes.Status401Unauthorized,
                ErrorType.Confilict => StatusCodes.Status409Conflict,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError
            };
            var problem = new ProblemDetails()
            {
                Status = statusCode,
                Title = firstError.code,
                Detail = firstError.descriptipn,
                Extensions = { ["errors"] = errors }
            };
            return new ObjectResult(problem) {StatusCode = statusCode };
        }
    }
}
