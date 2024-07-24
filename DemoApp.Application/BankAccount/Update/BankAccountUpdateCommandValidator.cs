using DemoApp.Domain.Entities;
using FluentValidation;

namespace DemoApp.Application.BankAccount.Update
{
    public class BankAccountUpdateCommandValidator : AbstractValidator<BankAccountUpdateCommand>
    {
		public BankAccountUpdateCommandValidator()
        {

            RuleFor(x => x.AccountNo)
               .NotEmpty()
               .MinimumLength(10)
               .MaximumLength(12);

            RuleFor(x => x.AccountName)
                .NotEmpty()
                .MinimumLength(5);

            RuleFor(x => x.BankName)
               .NotEmpty()
               .MinimumLength(5);

            RuleFor(x => x.AccountType)
               .NotEmpty()
               .Must(t => Enum.IsDefined(typeof(AccountType), t))
               .WithMessage("Invalid Account Type");
        }
    }
}

