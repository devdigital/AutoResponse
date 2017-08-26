using AutoResponse.Sample.Domain.Services;

namespace AutoResponse.WebApi2.IntegrationTests.Helpers
{
    public class TestSettingsService : ISettingsService
    {
        private readonly bool includeFullDetails;

        public TestSettingsService(bool includeFullDetails)
        {
            this.includeFullDetails = includeFullDetails;
        }

        public bool GetIncludeFullDetails()
        {
            return this.includeFullDetails;            
        }
    }
}