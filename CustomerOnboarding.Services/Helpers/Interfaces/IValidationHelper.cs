using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerOnboarding.Domain.DataTransferObjects;
// IValidationHelper.cs  

namespace CustomerOnboarding.Services.Helpers.Interfaces
{
    public interface IValidationHelper
    {
        Task ValidateCustomerAsync(CustomerDto customerDto);
    }
}
