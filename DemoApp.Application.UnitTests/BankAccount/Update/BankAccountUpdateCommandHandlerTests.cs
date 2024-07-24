using DemoApp.Application.BankAccount.Update;
using DemoApp.Domain.Common.Errors;

namespace DemoApp.Application.UnitTests.BankAccount.Update
{
    [Collection("MockRepositoryCollection")]
    public class BankAccountUpdateCommandHandlerTests
	{
        private readonly BankAccountUpdateCommandHandler _handler;
        private readonly MockRepositorySetup _mockSetup;

        public BankAccountUpdateCommandHandlerTests(MockRepositorySetup mockSetup)
		{
            _mockSetup = mockSetup;
            _handler = new BankAccountUpdateCommandHandler(_mockSetup.RepositoryMock.Object,
                _mockSetup.MapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldUpdateBankAccount_WhenAccountIsValid()
        {
            // Arrange
            var mocObject = _mockSetup.MockAccounts[0];
            var request = new BankAccountUpdateCommand(
                mocObject.Id,
                "Savings",
                "1234567890",
                "Test Account",
                "Test Bank"
                );

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsError);
            var updatedAccount = _mockSetup.MockAccounts.First(acc => acc.Id == request.Id);
            Assert.Equal(request.AccountNo, updatedAccount.AccountNo);
            Assert.Equal(request.AccountType, updatedAccount.AccountType.ToString());
            Assert.Equal(request.AccountName, updatedAccount.AccountName);
            Assert.Equal(request.BankName, updatedAccount.BankName);
        }

        [Fact]
        public async Task Handle_ShouldReturnNotFoundError_WhenInvalidAccount()
        {
            // Arrange
            var request = new BankAccountUpdateCommand(
                Guid.NewGuid(),
                "Savings",
                "1234567890",
                "Test Account",
                "Test Bank"
                );
            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsError);

            Assert.Equal(Errors.BankAccount.AccountNotFound, result.FirstError);
        }

        [Fact]
        public async Task Handle_ShouldReturnNotFoundError_WhenInvalidAccountType()
        {
            // Arrange
            var mocObject = _mockSetup.MockAccounts[0];
            var request = new BankAccountUpdateCommand(
                mocObject.Id,
                "Credit",
                "123456780",
                "Test Account",
                "Test Bank"
                );
            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsError);
        }
    }
}

