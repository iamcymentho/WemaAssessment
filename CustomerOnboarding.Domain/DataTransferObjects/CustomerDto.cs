using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.Domain.DataTransferObjects
{
   public class CustomerDto
    {
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string LGA { get; set; } = string.Empty;
    }
}
