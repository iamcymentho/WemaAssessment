using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.Services.Services.ExternalService.Responses
{
    public class Result
    {
        [JsonProperty("bankName")]
        public string BankName { get; set; }

        [JsonProperty("bankCode")]
        public string BankCode { get; set; }

        [JsonProperty("bankLogo")]
        public string BankLogo { get; set; }
    }

    public class GetAllBanksResponse
    {
        [JsonProperty("result")]
        public List<Result> Result { get; set; }

        [JsonProperty("errorMessage")]
        public object ErrorMessage { get; set; }

        [JsonProperty("errorMessages")]
        public object ErrorMessages { get; set; }

        [JsonProperty("hasError")]
        public bool HasError { get; set; }

        [JsonProperty("timeGenerated")]
        public DateTime TimeGenerated { get; set; }
    }
}
