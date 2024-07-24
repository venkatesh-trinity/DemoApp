using System;
using Xunit;
namespace DemoApp.Infrastructure.UnitTests.BankAccount
{
    [CollectionDefinition("RepositoryCollection")]
    public class RepositoryCollection : ICollectionFixture<RepositoryMockSetup>
    {
    }
}

