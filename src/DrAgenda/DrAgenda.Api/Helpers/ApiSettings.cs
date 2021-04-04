namespace DrAgenda.Api.Helpers
{
    public class ApiSettings
    {
        public ApiSettings(string apiKey)
        {
            ApiKey = apiKey;
        }

        public string ApiKey { get; set; }
    }
}
