using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerOnboarding.Services.Services.Interfaces;
using System.Collections.Concurrent;

namespace CustomerOnboarding.Services.Services.Implementations
{
    public class CustomerOtpService : ICustomerOtpService
    {
        private readonly ConcurrentDictionary<string, string> _otpStore = new();

        // Generates a 6-digit OTP and stores it temporarily
        public string GenerateOtp(string phoneNumber)
        {
            var otp = new Random().Next(100000, 999999).ToString();
            _otpStore[phoneNumber] = otp;
            Console.WriteLine($"Mock OTP for {phoneNumber}: {otp}"); // Mock sending OTP
            return otp;
        }

        // Validates the OTP for the given phone number
        public bool ValidateOtp(string phoneNumber, string otp)
        {
            if (_otpStore.TryGetValue(phoneNumber, out var storedOtp) && storedOtp == otp)
            {
                _otpStore.TryRemove(phoneNumber, out _); // Remove after successful validation
                return true;
            }
            return false;
        }
    }

}
