using AutoResponse.Core.Logging;
using AutoResponse.Core.Mappers;

namespace AutoResponse.AspNetCore
{
    public class AutoResponseOptions
    {
        public IApiEventHttpResponseMapper EventHttpResponseMapper { get; set; }

        public IAutoResponseLogger Logger { get; set; }

        public string DomainResultPropertyName { get; set; }
    }
}