using DemoApp.Api.Contracts;
using DemoApp.Application.BankAccount.Details;
using DemoApp.Application.BankAccount.List;
using DemoApp.Application.BankAccount.Update;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.Api.Controllers
{
    [Route("DemoApp")]
    public class BankAccountController: BaseController
	{
		public BankAccountController(ISender sender, IMapper mapper):
			base(sender, mapper)
		{
		}

        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BankAccountDetailsResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll([FromQuery] string type)
        {
            var query = new BankAccountsByTypeQuery(type);

            var result = await _sender.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<List<BankAccountDetailsResponse>>(result)),
                errors => Problem(errors)
                );
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BankAccountDetailsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute]Guid id)
        {
            var query = new BankAccountByIdQuery(id);

            var result = await _sender.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<BankAccountDetailsResponse>(result)),
                errors => Problem(errors)
                );
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BankAccountDetailsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateBankAccountRequest request)
        {
            var query = _mapper.Map<BankAccountUpdateCommand>((request, id));

            var result = await _sender.Send(query);

            return result.Match(
                result => Ok(_mapper.Map<BankAccountDetailsResponse>(result)),
                errors => Problem(errors)
                );
        }
    }
}

