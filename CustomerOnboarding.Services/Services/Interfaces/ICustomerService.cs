using CustomerOnboarding.Domain.DataTransferObjects;
using CustomerOnboarding.Domain.Entities;
using CustomerOnboarding.Domain.Common;

namespace CustomerOnboarding.Services.Services.Interfaces
{
    public interface ICustomerService
    {
        // Task<bool> OnboardCustomerAsync(CustomerDto customerDto);
        Task<Result> InitiateOnboardingAsync(CustomerDto customerDto);
        Task<Result> VerifyOtpAndCompleteOnboardingAsync(string phoneNumber, string otp);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
    }
}
