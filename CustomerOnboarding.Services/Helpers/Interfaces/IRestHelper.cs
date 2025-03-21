using CustomerOnboarding.Domain.Entities;

namespace CustomerOnboarding.Services.Helpers.Interfaces;
public interface IRestHelper
{
    Task<T> DoWebRequestAsync<T>(MyWemaBankLog log, string url, object request, string requestType, Dictionary<string, string> headers = null) where T : new();

}