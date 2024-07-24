namespace DemoApp.Application.BankAccount.Common
{
    public record BankAccountDetailsResult(Guid Id,
		string AccountType,
		string AccountNo, 
		string AccountName,
		string BankName,
		DateTime CreatedOn
        );
}

