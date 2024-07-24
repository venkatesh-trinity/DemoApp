using DemoApp.Application.BankAccount.Common;
using DemoApp.Application.Common.Interfaces.Persistance;
using MapsterMapper;
using Moq;
using Entities = DemoApp.Domain.Entities;

public class MockRepositorySetup
{
    public Mock<IBankAccountRepository> RepositoryMock { get; private set; }
    public Mock<IMapper> MapperMock { get; private set; }
    public List<Entities.BankAccount> MockAccounts { get; private set; }
    public List<BankAccountDetailsResult> MockAccountResponses { get; private set; }

    public MockRepositorySetup()
    {
        RepositoryMock = new Mock<IBankAccountRepository>();
        MapperMock = new Mock<IMapper>();

        // Initialize mock data
        MockAccounts = new List<Entities.BankAccount>
        {
            Entities.BankAccount.Create("123456789", "Savings", "John Doe", "Bank A"),
            Entities.BankAccount.Create("987654321", "Current", "Jane Doe", "Bank B")
        };

        MockAccountResponses = MockAccounts.Select(account => new BankAccountDetailsResult
        (
            account.Id,
            account.AccountType.ToString(),
            account.AccountNo,
            account.AccountName,
            account.BankName,
            account.CreatedOn
        )).ToList();

        // Setup repository mock
        RepositoryMock.Setup(repo => repo.GetBankAccounts(It.IsAny<string>()))
            .ReturnsAsync((string type) => MockAccounts.Where(acc => acc.AccountType.ToString() == type).ToList());

        RepositoryMock.Setup(repo => repo.GetBankAccount(It.IsAny<Guid>()))
            .ReturnsAsync((Guid id) => MockAccounts.FirstOrDefault(acc => acc.Id == id));

        // Setup Update method
        RepositoryMock.Setup(repo => repo.Update(It.IsAny<Entities.BankAccount>()))
            .Callback<Entities.BankAccount>(account =>
            {
                var existingAccount = MockAccounts.FirstOrDefault(acc => acc.Id == account.Id);
                if (existingAccount != null)
                {
                    existingAccount.AccountNo = account.AccountNo;
                    existingAccount.AccountType = account.AccountType;
                    existingAccount.AccountName = account.AccountName;
                    existingAccount.BankName = account.BankName;
                }
            });

        // Setup mapper mock
        MapperMock.Setup(mapper => mapper.Map<List<BankAccountDetailsResult>>(It.IsAny<List<Entities.BankAccount>>()))
            .Returns((List<Entities.BankAccount> accounts) => MockAccountResponses.Where(acc => accounts.Any(a => a.Id == acc.Id)).ToList());

        MapperMock.Setup(mapper => mapper.Map<BankAccountDetailsResult?>(It.IsAny<Entities.BankAccount>()))
            .Returns((Entities.BankAccount account) => MockAccountResponses.FirstOrDefault(acc => acc.Id == account.Id));
    }
}
