using DemoApp.Application.BankAccount.Details;
using DemoApp.Domain.Common.Errors;

namespace DemoApp.Application.UnitTests.BankAccount.Details
{
    [Collection("MockRepositoryCollection")]
    public class BankAccountByIdQueryHandlerTests
	{
        private readonly BankAccountByIdQueryHandler _handler;
        private readonly MockRepositorySetup _mockSetup;

        public BankAccountByIdQueryHandlerTests(MockRepositorySetup mockSetup)
		{
            _mockSetup = mockSetup;
            _handler = new BankAccountByIdQueryHandler(_mockSetup.RepositoryMock.Object,
                _mockSetup.MapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnBankAccount_WhenAccountExists()
        {
            // Arrange
            var request = new BankAccountByIdQuery(_mockSetup.MockAccounts[0].Id);
            var expResult = _mockSetup.MockAccountResponses.FirstOrDefault(m => m.Id == _mockSetup.MockAccounts[0].Id);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsError);
            Assert.Equal(expResult, result.Value);
        }

        [Fact]
        public async Task Handle_ShouldReturnNotFoundError_WhenInvalidAccount()
        {
            // Arrange
            var request = new BankAccountByIdQuery(Guid.NewGuid());
            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsError);

            Assert.Equal(Errors.BankAccount.AccountNotFound, result.FirstError);
        }
    }
}

