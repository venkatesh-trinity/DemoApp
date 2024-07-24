namespace DemoApp.Api.Contracts
{
    public record UpdateBankAccountRequest(string AccountType,
        string AccountNo,
        string AccountName,
        string BankName);
}

