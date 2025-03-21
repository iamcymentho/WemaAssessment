using CustomerOnboarding.Services.Helpers.Interfaces;
using Microsoft.Extensions.Logging;
using RestSharp;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using CustomerOnboarding.Domain.Entities;

namespace CustomerOnboarding.Services.Helpers.Implementations;
public class RestHelper : IRestHelper
{

    private readonly ILogger<RestHelper> _logger;
    private readonly IConfiguration _config;

    public RestHelper(IConfiguration config, ILogger<RestHelper> logger)
    {


        _config = config;
        _logger = logger;
    }

    public async Task<T> DoWebRequestAsync<T>(MyWemaBankLog log, string url, object request, string requestType, Dictionary<string, string> headers = null) where T : new()
    {
        var SDS = JsonConvert.SerializeObject(request);
        _logger.LogInformation("URL: " + url + " " + JsonConvert.SerializeObject(request));
        T result = new T();

        Method method = requestType.ToLower() == "post" ? Method.Post : Method.Get;
        var client = new RestClient(url);
        var restRequest = new RestRequest(url, method);
        if (method == Method.Post)
        {
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddJsonBody(request);

        }

        if (headers != null)
        {
            foreach (var item in headers)
            {
                restRequest.AddHeader(item.Key, item.Value);
            }
        }

        try
        {
            RestResponse<T> response = await client.ExecuteAsync<T>(restRequest);

            _logger.LogInformation("URL: " + url + " " + response.Content);
            if (!response.IsSuccessful)
            {
                log.ResponseDetails = $"URL: {url} {response.Content}";
            }

            result = JsonConvert.DeserializeObject<T>(response.Content);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            log.ResponseDetails = $"URL: {url} {ex.Message}";

            return result;
        }
    }
}
