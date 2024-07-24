using DemoApp.Application.BankAccount.List;
using DemoApp.Domain.Common.Errors;

namespace DemoApp.Application.UnitTests.BankAccount.List
{
    [Collection("MockRepositoryCollection")]
    public class BankAccountsByTypeQueryHandlerTests
	{
		private readonly BankAccountsByTypeQueryHandler _handler;
        private readonly MockRepositorySetup _mockSetup;

        public BankAccountsByTypeQueryHandlerTests(MockRepositorySetup mockSetup)
		{
            _mockSetup = mockSetup;
			_handler = new BankAccountsByTypeQueryHandler(_mockSetup.RepositoryMock.Object,
                _mockSetup.MapperMock.Object);
		}

        
        [Fact]
        public async Task Handle_ShouldReturnBankAccounts_WhenAccountsExist()
        {
            // Arrange
            var request = new BankAccountsByTypeQuery ("Savings" );

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsError);
            Assert.Single(result.Value);
            Assert.Equal("Savings", result.Value[0].AccountType);
        }

        
        [Fact]
        public async Task Handle_ShouldReturnNoDataError_WhenNoAccountsExist()
        {
            // Arrange
            var request = new BankAccountsByTypeQuery("Checking");
            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsError);

            Assert.Equal(Errors.BankAccount.AccountNoData, result.FirstError);
        }
    }
}

