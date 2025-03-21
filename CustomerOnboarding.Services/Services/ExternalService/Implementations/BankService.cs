using CustomerOnboarding.Domain.Entities;
using CustomerOnboarding.Services.Helpers.Interfaces;
using CustomerOnboarding.Services.Services.ExternalService.Interfaces;
using CustomerOnboarding.Services.Services.ExternalService.Responses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CustomerOnboarding.Services.Services.ExternalService.Implementations
{
    public class BankService : IBankService
    {
        private readonly IRestHelper _restHelper;
        private readonly IConfiguration _configuration;
        private readonly ILogger<BankService> _logger;

        public BankService(IRestHelper restHelper, IConfiguration configuration, ILogger<BankService> logger)
        {
            _restHelper = restHelper;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<List<Result>> GetAllBanksAsync() 
        {
            var log = new MyWemaBankLog
            {
                ServiceName = "GetAllBanks",
                Endpoint = _configuration["BankApi:GetAllBanksUrl"],
                RequestDate = DateTime.UtcNow.ToString()
            };

            try
            {
                var url = _configuration["BankApi:GetAllBanksUrl"];
                if (string.IsNullOrEmpty(url))
                {
                    throw new InvalidOperationException("Bank API URL is not configured.");
                }

                // Consuming external API  
                var response = await _restHelper.DoWebRequestAsync<GetAllBanksResponse>(log, url, null, HttpMethod.Get.Method);

                if (response.HasError)
                {
                    _logger.LogError("Error fetching banks: {ErrorMessage}", response.ErrorMessage);
                    return new List<Result>(); 
                }

                return response.Result; 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while fetching banks.");
                return new List<Result>(); 
            }
        }
    }
}