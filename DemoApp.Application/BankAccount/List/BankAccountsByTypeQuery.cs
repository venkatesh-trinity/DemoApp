using DemoApp.Application.BankAccount.Common;
using ErrorOr;
using MediatR;

namespace DemoApp.Application.BankAccount.List
{
    public record BankAccountsByTypeQuery(string AccountType): IRequest<ErrorOr<List<BankAccountDetailsResult>>>;
}

