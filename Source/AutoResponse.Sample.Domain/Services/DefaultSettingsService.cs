namespace AutoResponse.Sample.Domain.Services
{
    public class DefaultSettingsService : ISettingsService
    {
        public bool GetIncludeFullDetails()
        {
            return false;
        }
    }
}