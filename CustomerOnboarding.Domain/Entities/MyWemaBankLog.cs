namespace CustomerOnboarding.Domain.Entities
{
    public class MyWemaBankLog
    {
        public string ServiceName { get; set; } = "GetAllBanks";
        public string Endpoint { get; set; }
        public string RequestDate { get; set; }
        public string ResponseDate { get; set; }
        public string RequestDetails { get; set; }
        public string Response { get; set; } = "Failed";
        public string ResponseDetails { get; set; }
    }
}

