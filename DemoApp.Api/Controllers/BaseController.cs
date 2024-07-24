using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DemoApp.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class BaseController : ControllerBase
    {
        protected readonly ISender _sender;
        protected readonly IMapper _mapper;

        public BaseController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [NonAction]
        protected IActionResult Problem(List<Error> errors)
        {
            if (errors.Count == 0)
            {
                return Problem();
            }

            if (errors.All(error => error.Type == ErrorType.Validation))
            {
                return ValidationError(errors);
            }

            var firstError = errors[0];

            return Problem(firstError);
        }

        private IActionResult Problem(Error error)
        {
            var statusCode = error.Type switch
            {
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                _ => StatusCodes.Status500InternalServerError,

            };

            return Problem(statusCode: statusCode, title: error.Description);
        }

        private IActionResult ValidationError(List<Error> errors)
        {
            var modelStateDictionary = new ModelStateDictionary();

            foreach (var error in errors)
            {
                modelStateDictionary.AddModelError(
                    error.Code,
                    error.Description
                    );
            }
            return ValidationProblem(modelStateDictionary);
        }
    }
}

