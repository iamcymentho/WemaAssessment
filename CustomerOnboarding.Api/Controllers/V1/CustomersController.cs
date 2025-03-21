using CustomerOnboarding.Domain.DataTransferObjects;
using CustomerOnboarding.Services.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerOnboarding.Api.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IBankMiddlewareService _bankMiddlewareService;

        public CustomersController(ICustomerService customerService, IBankMiddlewareService bankMiddlewareService)
        {
            _customerService = customerService;
            _bankMiddlewareService = bankMiddlewareService;
        }

        // Step 1: Onboard Customer (generate OTP)  
        [HttpPost("wema-Bank/v1/initiate-onboarding")]
        public async Task<IActionResult> OnboardCustomer([FromBody] CustomerDto customerDto)
        {
            var result = await _customerService.InitiateOnboardingAsync(customerDto);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok("OTP sent to your phone number. Please verify.");
        }

        // Step 2: Verify OTP  
        [HttpPost("wema-bank/v1/verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromQuery] string phoneNumber, [FromQuery] string otp)
        {
            var result = await _customerService.VerifyOtpAndCompleteOnboardingAsync(phoneNumber, otp);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok("Customer onboarded successfully.");
        }


        [HttpGet("wema-bank/v1/get-all-customers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var result = await _customerService.GetAllCustomersAsync();

            // Check if the result is successful
            if (result == null || !result.Any())
            {
                return NotFound("No customers found.");
            }

            return Ok(result); 
        }

        [HttpGet("get-all-banks")]
        public async Task<ActionResult<List<BankDto>>> GetAllBanksAsync()
        {
            var banks = await _bankMiddlewareService.GetAllBanksAsync();
            if (banks == null || banks.Count == 0)
            {
                return NotFound("No banks found.");
            }
            return Ok(banks);
        }

    }
}
