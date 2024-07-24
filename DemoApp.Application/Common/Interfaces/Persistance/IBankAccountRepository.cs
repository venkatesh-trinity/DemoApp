using Entities = DemoApp.Domain.Entities;

namespace DemoApp.Application.Common.Interfaces.Persistance
{
	public interface IBankAccountRepository
    {
        Task Update(Entities.BankAccount account);
        Task<Entities.BankAccount?> GetBankAccount(Guid bankAccountId);
        Task<Entities.BankAccount?> GetBankAccountByNumber(string accountNo);
        Task<List<Entities.BankAccount>?> GetBankAccounts(string accountType);
    }
}

