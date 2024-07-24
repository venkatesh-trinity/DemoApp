using DemoApp.Infrastructure.Persistance;
using DemoApp.Infrastructure.Persistance.Interceptors;
using DemoApp.Infrastructure.Persistance.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using Entities = DemoApp.Domain.Entities;

namespace DemoApp.Infrastructure.UnitTests.BankAccount
{
    public class RepositoryMockSetup
    {
        public AppDbContext DbContext { get; private set; }
        public BankAccountRepository Repository { get; private set; }

        public RepositoryMockSetup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            DbContext = new AppDbContext(options, new Mock<PublishDomainEventsInterceptor>(Mock.Of<IPublisher>()).Object);
            Repository = new BankAccountRepository(DbContext);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            DbContext.BankAccounts.AddRange(new List<Entities.BankAccount>
            {
                Entities.BankAccount.Create("123456789", "Savings", "John Doe", "Bank A"),
                Entities.BankAccount.Create("987654321", "Current", "Jane Doe", "Bank B")
            });

            DbContext.SaveChanges();
        }
    }
}

