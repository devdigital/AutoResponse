using AutoResponse.Core.Logging;
using AutoResponse.Core.Mappers;
using Microsoft.AspNetCore.Builder;

namespace AutoResponse.AspNetCore
{
    public static class IApplicationBuilderExtensions
    {
        public static void UseAutoResponse(this IApplicationBuilder applicationBuilder)
        {   
            UseAutoResponse(applicationBuilder, new AutoResponseOptions());            
        }

        public static void UseAutoResponse(this IApplicationBuilder applicationBuilder, AutoResponseOptions options)
        {
            var defaultMapper = new AutoResponseApiEventHttpResponseMapper(
                new AspNetCoreContextResolver());

            var defaultLogger = new NullAutoResponseLogger();
            const string defaultDomainResultPropertyName = "Event";

            var mapper = options.EventHttpResponseMapper ?? defaultMapper;
            var logger = options.Logger ?? defaultLogger;
            var domainResultPropertyName = string.IsNullOrWhiteSpace(options.DomainResultPropertyName)
                ? defaultDomainResultPropertyName
                : options.DomainResultPropertyName;

            applicationBuilder.UseMiddleware<AutoResponseExceptionMiddleware>(
                mapper,
                logger,
                domainResultPropertyName);
        }
    }
}