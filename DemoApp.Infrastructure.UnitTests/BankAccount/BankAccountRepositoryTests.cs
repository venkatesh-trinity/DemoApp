namespace DemoApp.Infrastructure.UnitTests.BankAccount
{
    [Collection("RepositoryCollection")]
    public class BankAccountRepositoryTests
	{
        private readonly RepositoryMockSetup _mockSetup;

        public BankAccountRepositoryTests(RepositoryMockSetup mockSetup)
		{
            _mockSetup = mockSetup;
        }

        [Fact]
        public async Task GetBankAccount_ShouldReturnAccount_WhenAccountExists()
        {
            // Arrange
            var accountId = _mockSetup.DbContext.BankAccounts.First().Id;

            // Act
            var result = await _mockSetup.Repository.GetBankAccount(accountId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(accountId, result.Id);
        }

        [Fact]
        public async Task GetBankAccountByNumber_ShouldReturnAccount_WhenAccountExists()
        {
            // Arrange
            var accountNo = _mockSetup.DbContext.BankAccounts.First().AccountNo;

            // Act
            var result = await _mockSetup.Repository.GetBankAccountByNumber(accountNo);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(accountNo, result.AccountNo);
        }

        [Fact]
        public async Task GetBankAccounts_ShouldReturnAccounts_WhenAccountsExist()
        {
            // Arrange
            var accountType = "Savings";

            // Act
            var result = await _mockSetup.Repository.GetBankAccounts(accountType);

            // Assert
            Assert.NotNull(result);
            Assert.All(result, acc => Assert.Equal(accountType, acc.AccountType.ToString(), ignoreCase: true));
        }

        [Fact]
        public async Task GetBankAccounts_ShouldReturnNoRecrods_WhenAccountsNotExist()
        {
            // Arrange
            var accountType = "Test";

            // Act
            var result = await _mockSetup.Repository.GetBankAccounts(accountType);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Capacity);
        }

        [Fact]
        public async Task Update_ShouldCallSaveChanges_WhenAccountIsUpdated()
        {
            // Arrange
            var account = _mockSetup.DbContext.BankAccounts.First();
            account.AccountName = "Updated Name";

            // Act
            await _mockSetup.Repository.Update(account);

            // Assert
            var updatedAccount = _mockSetup.DbContext.BankAccounts.First(a => a.Id == account.Id);
            Assert.Equal("Updated Name", updatedAccount.AccountName);
        }
    }
}

