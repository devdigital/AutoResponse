namespace AutoResponse.Owin
{
    using AutoResponse.Core.Logging;
    using AutoResponse.Core.Mappers;

    public class AutoResponseOptions
    {
        public IApiEventHttpResponseMapper EventHttpResponseMapper { get; set; }

        public IAutoResponseLogger Logger { get; set; }

        public string DomainResultPropertyName { get; set; }
    }
}