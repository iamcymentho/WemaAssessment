using CustomerOnboarding.Domain.DataTransferObjects;
using CustomerOnboarding.Services.Services.ExternalService.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.Services.Services.ExternalService.Interfaces
{
        public interface IBankService
        {
             Task<List<Result>> GetAllBanksAsync();
        }
}
