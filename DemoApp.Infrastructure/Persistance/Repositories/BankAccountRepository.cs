using DemoApp.Application.Common.Interfaces.Persistance;
using DemoApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Infrastructure.Persistance.Repositories
{
    public class BankAccountRepository: IBankAccountRepository
    {
        private readonly AppDbContext _dbContext;

        public BankAccountRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BankAccount?> GetBankAccount(Guid bankAccountId)
        {
            BankAccount? account = await _dbContext.BankAccounts.FirstOrDefaultAsync(t => t.Id == bankAccountId);

            return account;
        }

        public async Task<BankAccount?> GetBankAccountByNumber(string accountNo)
        {
            BankAccount? account = await
                _dbContext.BankAccounts
                .FirstOrDefaultAsync(t => t.AccountNo == accountNo);

            return account;
        }

        public async Task<List<BankAccount>?> GetBankAccounts(string accountType)
        {
            return await _dbContext.BankAccounts
                        .AsNoTracking()
                        .Where(a => a.AccountType.ToString().ToLower() == accountType.ToLower())
                        .ToListAsync();
        }

        public async Task Update(BankAccount account)
        {
            _dbContext.Update(account);
            await _dbContext.SaveChangesAsync();
        }
    }
}

