using CustomerOnboarding.Domain.DataTransferObjects;
using CustomerOnboarding.Services.Services.ExternalService.Interfaces;
using CustomerOnboarding.Services.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace CustomerOnboarding.Services.Services.Implementations
{
    public class BankMiddlewareService : IBankMiddlewareService
    {
        private readonly IBankService _bankService;
        private readonly ILogger<BankMiddlewareService> _logger;

        public BankMiddlewareService(IBankService bankService, ILogger<BankMiddlewareService> logger)
        {
            _bankService = bankService;
            _logger = logger;
        }
        public async Task<List<BankDto>> GetAllBanksAsync()
        {
            try
            { 
                var bankResults = await _bankService.GetAllBanksAsync(); 

                if (bankResults == null || !bankResults.Any())
                {
                    throw new InvalidOperationException("No banks found from the bank service.");
                }

                // Map the Result to BankDto  
                var bankDtos = bankResults.Select(bank => new BankDto
                {
                    BankName = bank.BankName,
                    BankCode = bank.BankCode,
                    BankLogo = bank.BankLogo
                }).ToList();

                return bankDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all banks.");
                return new List<BankDto>(); 
            }
        }
    }
}
