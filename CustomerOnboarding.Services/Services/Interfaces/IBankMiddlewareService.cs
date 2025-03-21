using CustomerOnboarding.Domain.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.Services.Services.Interfaces
{
    public interface IBankMiddlewareService
    {
        Task<List<BankDto>> GetAllBanksAsync();
    }
}
