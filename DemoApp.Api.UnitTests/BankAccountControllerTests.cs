using DemoApp.Api.Contracts;
using DemoApp.Api.Controllers;
using DemoApp.Application.BankAccount.Common;
using DemoApp.Application.BankAccount.Details;
using DemoApp.Application.BankAccount.List;
using DemoApp.Application.BankAccount.Update;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Errors = DemoApp.Domain.Common.Errors;

namespace DemoApp.Api.UnitTests
{
    public class BankAccountControllerTests
    {
        private readonly Mock<ISender> _senderMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly BankAccountController _controller;

        public BankAccountControllerTests()
        {
            _senderMock = new Mock<ISender>();
            _mapperMock = new Mock<IMapper>();
            _controller = new BankAccountController(_senderMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkResult_WithBankAccounts()
        {
            // Arrange
            var type = "Savings";
            var date = DateTime.UtcNow;
            var id = Guid.NewGuid();
            var query = new BankAccountsByTypeQuery(type);

            var bankAccounts = new List<BankAccountDetailsResult>
            {
                new BankAccountDetailsResult(id, type, "123456","John Doe", "Bank A", date)
            };
            
            var mappedResponse = new List<BankAccountDetailsResponse>
            {
                new BankAccountDetailsResponse(id, type, "123456", "John Doe", "Bank A", date)
            };

            // Mock the ISender
            _senderMock.Setup(x => x.Send(query,
                It.IsAny<CancellationToken>())).ReturnsAsync(bankAccounts);

            // Mock the mapper
            _mapperMock.Setup(m => m.Map<List<BankAccountDetailsResponse>>(It.IsAny<List<BankAccountDetailsResult>>()))
                .Returns(mappedResponse);

            // Act
            var result = await _controller.GetAll(type);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<BankAccountDetailsResponse>>(actionResult.Value);
            Assert.Equal(mappedResponse, returnValue);
        }

        [Fact]
        public async Task GetAll_ShouldReturnNotFound_WhenNoBankAccounts()
        {
            // Arrange
            var type = "Savings";
            var query = new BankAccountsByTypeQuery(type);

            // Mock the ISender
            _senderMock.Setup(x => x.Send(query,
                It.IsAny<CancellationToken>())).ReturnsAsync(Errors.Errors.BankAccount.AccountNoData);

            // Act
            var result = await _controller.GetAll(type);

            // Assert
            var actionResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(404, actionResult.StatusCode);
        }

        [Fact]
        public async Task GetById_ShouldReturnOkResult_WithBankAccount()
        {
            // Arrange
            var date = DateTime.UtcNow;
            var id = Guid.NewGuid();
            var bankAccounts = new BankAccountDetailsResult(id, "Savings", "123456", "John Doe", "Bank A", date);

            var query = new BankAccountByIdQuery(id);

            var mappedResponse = new BankAccountDetailsResponse(id, "Savings", "123456", "John Doe", "Bank A", date);

            // Mock the ISender
            _senderMock.Setup(x => x.Send(query,
                It.IsAny<CancellationToken>())).ReturnsAsync(bankAccounts);

            // Mock the mapper
            _mapperMock.Setup(m => m.Map<BankAccountDetailsResponse>(It.IsAny<BankAccountDetailsResult>()))
                .Returns(mappedResponse);

            // Act
            var result = await _controller.Get(id);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<BankAccountDetailsResponse>(actionResult.Value);
            Assert.Equal(mappedResponse, returnValue);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenNoBankAccounts()
        {
            // Arrange
            var id = Guid.NewGuid();
            var query = new BankAccountByIdQuery(id);

            _senderMock.Setup(x => x.Send(query,
                It.IsAny<CancellationToken>())).ReturnsAsync(Errors.Errors.BankAccount.AccountNotFound);

            // Act
            var result = await _controller.Get(id);

            // Assert
            var actionResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(404, actionResult.StatusCode);
        }

        
        [Fact]
        public async Task Update_ShouldReturnOkResult_WhenSuccessful()
        {
            // Arrange
            var id = Guid.NewGuid();
            var date = DateTime.UtcNow;
            var request = new UpdateBankAccountRequest("Savings", "123456", "John Doe", "Bank A");
            var command = new BankAccountUpdateCommand(id, "Savings", "123456", "John Doe", "Bank A");
            var updatedAccount = new BankAccountDetailsResult(id, "Savings", "123456", "John Doe", "Bank A", date); 
            var mappedResponse = new BankAccountDetailsResponse(id, "Savings", "123456", "John Doe", "Bank A", date);

            // Mock the IMapper.Map method
            _mapperMock.Setup(m => m.Map<BankAccountUpdateCommand>(
                (It.IsAny<(UpdateBankAccountRequest, Guid)>())))
                .Returns(command);

            // Mock the ISender.Send method to return a successful result
            _senderMock.Setup(x => x.Send(command,
                It.IsAny<CancellationToken>())).ReturnsAsync(updatedAccount);

            // Mock the IMapper.Map method
            _mapperMock.Setup(m => m.Map<BankAccountDetailsResponse>(It.IsAny<BankAccountDetailsResult>()))
                .Returns(mappedResponse);

            // Act
            var result = await _controller.Update(id, request);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<BankAccountDetailsResponse>(actionResult.Value);
            Assert.Equal(mappedResponse, returnValue);
        }

        [Fact]
        public async Task Update_ShouldReturnNotFound_WhenInvalidAccountId()
        {
            // Arrange
            var id = Guid.NewGuid();
            var request = new UpdateBankAccountRequest("Savings", "123456", "John Doe", "Bank A");
            var command = new BankAccountUpdateCommand(id, "Savings", "123456", "John Doe", "Bank A");
            var query = new BankAccountByIdQuery(id);

            // Mock the IMapper.Map method
            _mapperMock.Setup(m => m.Map<BankAccountUpdateCommand>(
                (It.IsAny<(UpdateBankAccountRequest, Guid)>())))
                .Returns(command);

            // Mock the ISender
            _senderMock.Setup(x => x.Send(query,
                It.IsAny<CancellationToken>())).ReturnsAsync(Errors.Errors.BankAccount.AccountNotFound);

            // Act
            var result = await _controller.Get(id);

            // Assert
            var actionResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(404, actionResult.StatusCode);
        }
    }
}

