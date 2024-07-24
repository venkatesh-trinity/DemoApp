using DemoApp.Application.BankAccount.Common;
using ErrorOr;
using MediatR;

namespace DemoApp.Application.BankAccount.Update
{
    public record BankAccountUpdateCommand(Guid Id,
        string AccountType,
        string AccountNo,
        string AccountName,
        string BankName
        ): IRequest<ErrorOr<BankAccountDetailsResult>>;
}

