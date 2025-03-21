using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.Domain.DataTransferObjects
{
    public class BankDto
    {
        public string BankName { get; set; }
        public string BankCode { get; set; }
        public string BankLogo { get; set; }
    }

    public class BankApiResponse
    {
        public BankDto Result { get; set; }
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
    }
}
