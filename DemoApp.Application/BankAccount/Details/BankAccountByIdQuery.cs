using DemoApp.Application.BankAccount.Common;
using ErrorOr;
using MediatR;

namespace DemoApp.Application.BankAccount.Details
{
	public record BankAccountByIdQuery(Guid Id): IRequest<ErrorOr<BankAccountDetailsResult>>;
}

