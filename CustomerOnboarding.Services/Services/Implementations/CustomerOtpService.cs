using CustomerOnboarding.Services.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace CustomerOnboarding.Services.Services.Implementations
{
    public class CustomerOtpService : ICustomerOtpService
    {
        private readonly IMemoryCache _memoryCache;
        public CustomerOtpService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public string GenerateOtp(string phoneNumber)
        {
            var otp = new Random().Next(100000, 999999).ToString();

            _memoryCache.Set(phoneNumber, otp, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) 
            });
            Console.WriteLine($"Generated OTP for {phoneNumber}: {otp}");
            return otp;
        }

        public bool ValidateOtp(string phoneNumber, string otp)
        {
            
            if (_memoryCache.TryGetValue(phoneNumber, out string storedOtp))
            {
                Console.WriteLine($"Stored OTP for {phoneNumber}: {storedOtp}");

                if (storedOtp == otp)
                {
                    
                    _memoryCache.Remove(phoneNumber);
                    return true;
                }
                else
                {
                    Console.WriteLine($"Invalid OTP provided for {phoneNumber}: {otp}");
                }
            }
            else
            {
                Console.WriteLine($"No OTP found for {phoneNumber}");
            }

            return false;
        }
    }
}

