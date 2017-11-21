namespace AutoResponse.Owin
{
    using AutoResponse.Core.Logging;
    using AutoResponse.Core.Mappers;

    using global::Owin;

    public static class IAppBuilderExtensions
    {
        public static void UseAutoResponse(this IAppBuilder appBuilder)
        {   
            UseAutoResponse(appBuilder, new AutoResponseOptions());            
        }

        public static void UseAutoResponse(this IAppBuilder appBuilder, AutoResponseOptions options)
        {
            var defaultMapper = new AutoResponseApiEventHttpResponseMapper(new OwinContextResolver());
            var defaultLogger = new NullAutoResponseLogger();
            var defaultDomainResultPropertyName = "Event";

            var mapper = options.EventHttpResponseMapper ?? defaultMapper;
            var logger = options.Logger ?? defaultLogger;
            var domainResultPropertyName = string.IsNullOrWhiteSpace(options.DomainResultPropertyName)
                ? defaultDomainResultPropertyName
                : options.DomainResultPropertyName;

            appBuilder.Use<AutoResponseExceptionMiddleware>(mapper, logger, domainResultPropertyName);
        }
    }
}