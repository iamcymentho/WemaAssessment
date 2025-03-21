using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.Domain.Entities
{
    public class OnboardingResponse
    {
        public string Message { get; set; }
        public string Otp { get; set; } // OTP field
    }

}
