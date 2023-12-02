namespace dotnet_salary_master.Models
{
    public class AppSettings
    {
        public string Secret { get; set; } = String.Empty;

        public string ApiCorsPolicy { get; set; } = String.Empty;

        public string BearerValidIssuer { get; set; } = String.Empty;

        public string BearerValidAudience { get; set; } = String.Empty;
    }
}
