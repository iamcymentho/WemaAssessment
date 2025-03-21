using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.Services.Services.Interfaces
{
    public interface ICustomerOtpService
    {
        string GenerateOtp(string phoneNumber);
        bool ValidateOtp(string phoneNumber, string otp);
    }
}
