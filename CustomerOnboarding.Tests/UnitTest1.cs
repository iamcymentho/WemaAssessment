using Moq;
using Microsoft.AspNetCore.Mvc;
using CustomerOnboarding.Api.Controllers.V1;
using CustomerOnboarding.Services.Services.Interfaces;
using CustomerOnboarding.Domain.DataTransferObjects;
using CustomerOnboarding.Domain.Common;
using CustomerOnboarding.Domain.Entities;

namespace CustomerOnboarding.Tests
{
    public class CustomerControllerTests
    {
        private readonly Mock<ICustomerService> _mockCustomerService;
        private readonly Mock<IBankMiddlewareService> _mockBankMiddlewareService;
        private readonly CustomersController _controller;

        public CustomerControllerTests()
        {
            _mockCustomerService = new Mock<ICustomerService>();
            _mockBankMiddlewareService = new Mock<IBankMiddlewareService>();
            _controller = new CustomersController(_mockCustomerService.Object, _mockBankMiddlewareService.Object);
        }


        [Fact]
        public async Task OnboardCustomer_ShouldReturnOk_WhenOtpIsSent()
        {
            // Arrange
            var customerDto = new CustomerDto { PhoneNumber = "1234567890", Email = "test@example.com", Password = "Password123", State = "State", LGA = "LGA" };
            _mockCustomerService.Setup(x => x.InitiateOnboardingAsync(It.IsAny<CustomerDto>()))
                .ReturnsAsync(new Result { Success = true, Message = "OTP sent" });

            // Act
            var result = await _controller.OnboardCustomer(customerDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OTP sent to your phone number. Please verify.", okResult.Value);
        }

        [Fact]
        public async Task OnboardCustomer_ShouldReturnBadRequest_WhenOtpFails()
        {
            // Arrange
            var customerDto = new CustomerDto { PhoneNumber = "1234567890", Email = "test@example.com", Password = "Password123", State = "State", LGA = "LGA" };
            _mockCustomerService.Setup(x => x.InitiateOnboardingAsync(It.IsAny<CustomerDto>()))
                .ReturnsAsync(new Result { Success = false, Message = "Error occurred" });

            // Act
            var result = await _controller.OnboardCustomer(customerDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Error occurred", badRequestResult.Value);
        }

        [Fact]
        public async Task VerifyOtp_ShouldReturnOk_WhenOtpIsValid()
        {
            // Arrange
            string phoneNumber = "1234567890";
            string otp = "123456";
            _mockCustomerService.Setup(x => x.VerifyOtpAndCompleteOnboardingAsync(phoneNumber, otp))
                .ReturnsAsync(new Result { Success = true, Message = "Customer onboarded successfully." });

            // Act
            var result = await _controller.VerifyOtp(phoneNumber, otp);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Customer onboarded successfully.", okResult.Value);
        }

        [Fact]
        public async Task VerifyOtp_ShouldReturnBadRequest_WhenOtpIsInvalid()
        {
            // Arrange
            string phoneNumber = "1234567890";
            string otp = "654321";
            _mockCustomerService.Setup(x => x.VerifyOtpAndCompleteOnboardingAsync(phoneNumber, otp))
                .ReturnsAsync(new Result { Success = false, Message = "Invalid OTP" });

            // Act
            var result = await _controller.VerifyOtp(phoneNumber, otp);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid OTP", badRequestResult.Value);
        }

        [Fact]
        public async Task GetAllCustomers_ShouldReturnNotFound_WhenNoCustomersExist()
        {
            // Arrange  
            _mockCustomerService.Setup(x => x.GetAllCustomersAsync())
                .ReturnsAsync(Enumerable.Empty<Customer>());  

            // Act  
            var result = await _controller.GetAllCustomers();

            // Assert  
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("No customers found.", notFoundResult.Value);
        }


        [Fact]
        public async Task GetAllBanks_ShouldReturnOk_WhenBanksExist()
        {
            // Arrange  
            var mockBanks = new List<BankDto>
    {
        new BankDto { BankCode = "673839", BankName = "Bank1", BankLogo = "mylogo" },
        new BankDto { BankCode = "767468", BankName = "Bank2", BankLogo = "mylogo" }
    };

            // Setup the mock to return the list of banks  
            _mockBankMiddlewareService.Setup(service => service.GetAllBanksAsync())
                .ReturnsAsync(mockBanks);

            // Act  
            var result = await _controller.GetAllBanksAsync();

            
            var actionResult = Assert.IsType<ActionResult<List<BankDto>>>(result);

            // Now we check that the value returned is a List<BankDto>  
            var returnedBanks = Assert.IsType<List<BankDto>>(actionResult.Value);
            Assert.Equal(2, returnedBanks.Count);
        }

        [Fact]
        public async Task GetAllBanks_ShouldReturnNotFound_WhenNoBanksExist()
        {
            // Arrange  
            _mockBankMiddlewareService.Setup(service => service.GetAllBanksAsync())
                .ReturnsAsync(new List<BankDto>());  // Return an empty list  

            // Act  
            var result = await _controller.GetAllBanksAsync();

            // Assert  
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("No banks found.", notFoundResult.Value);
        }
    }
}
