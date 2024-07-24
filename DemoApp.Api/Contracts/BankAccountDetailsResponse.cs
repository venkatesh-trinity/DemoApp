namespace DemoApp.Api.Contracts
{
    public record BankAccountDetailsResponse(Guid Id,
        string AccountType,
        string AccountNo,
        string AccountName,
        string BankName,
        DateTime CreatedOn
        );
}

