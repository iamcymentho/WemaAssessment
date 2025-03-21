using CustomerOnboarding.Domain.DataTransferObjects;
using CustomerOnboarding.Services.Services.Interfaces;
using BCrypt.Net;
using CustomerOnboarding.Data.Repositories.Interfaces;
using AutoMapper;
using CustomerOnboarding.Domain.Entities;
using CustomerOnboarding.Domain.Common;
using System.Collections.Concurrent;
using CustomerOnboarding.Domain.DataTransferObjects.DtoValidators;
using CustomerOnboarding.Services.Helpers.Interfaces;

namespace CustomerOnboarding.Services.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerOtpService _otpService;
        private readonly IValidationHelper _validationHelper;
        private readonly IMapper _mapper;
        // In-memory cache for temporary customer data  
        private static readonly ConcurrentDictionary<string, CustomerDto> _customerCache = new();

        public CustomerService(ICustomerRepository customerRepository,
                               ICustomerOtpService otpService,
                               IMapper mapper,
                               IValidationHelper validationHelper)
        {
            _customerRepository = customerRepository;
            _otpService = otpService;
            _mapper = mapper;
            _validationHelper = validationHelper;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync() =>
            await _customerRepository.GetAllCustomersAsync();

        public async Task<Result> InitiateOnboardingAsync(CustomerDto customerDto)
        {
            //validating the customerDTO  
            await _validationHelper.ValidateCustomerAsync(customerDto);

            // Validate State and LGA Mapping  
            if (!await _customerRepository.IsValidStateAndLGAAsync(customerDto.State, customerDto.LGA))
                 return new Result { Success = false, Message = "Invalid state and LGA combination." };
                
            // Check if customer already exists  
            var existingCustomer = await _customerRepository.GetByPhoneNumberAsync(customerDto.PhoneNumber);
            if (existingCustomer != null) 
                 return new Result { Success = false, Message = "Customer already exists." }; ;

            // Generate and send OTP  
            string otpSent =  _otpService.GenerateOtp(customerDto.PhoneNumber);
            if (string.IsNullOrEmpty(otpSent))
            {
                return new Result { Success = false, Message = "Failed to send OTP." };
            }

            // Cache the customer DTO during the onboarding process  
            _customerCache[customerDto.PhoneNumber] = customerDto;

            return new Result { Success = true, Message = "OTP sent successfully." };
        }

        public async Task<Result> VerifyOtpAndCompleteOnboardingAsync(string phoneNumber, string otp)
        {
            // Validate OTP  
            bool isVerified =  _otpService.ValidateOtp(phoneNumber, otp);
            if (!isVerified)
            {
                return new Result { Success = false, Message = "Invalid OTP." };
            }

            // Retrieve the customer DTO from cache  
            if (!_customerCache.TryGetValue(phoneNumber, out var customerDto))
            {
                return new Result { Success = false, Message = "Customer data not found." };
            }

            // Map CustomerDto to Customer  
            var newCustomer = _mapper.Map<Customer>(customerDto);
            newCustomer.IsVerified = true;
            newCustomer.PasswordHash = BCrypt.Net.BCrypt.HashPassword(customerDto.Password); // Set the PasswordHash  

            await _customerRepository.AddCustomerAsync(newCustomer);

            // Optionally, remove the DTO from the cache after processing  
            _customerCache.TryRemove(phoneNumber, out _);

            return new Result { Success = true, Message = "Customer onboarded successfully." };
        }

    }

}
