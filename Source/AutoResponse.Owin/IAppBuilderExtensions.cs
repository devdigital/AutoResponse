namespace AutoResponse.Owin
{
    using AutoResponse.Core.Mappers;

    using global::Owin;

    public static class IAppBuilderExtensions
    {
        public static void UseAutoResponse(this IAppBuilder appBuilder, IApiEventHttpResponseMapper mapper = null)
        {
            appBuilder.Use<AutoResponseExceptionMiddleware>(
                mapper ?? new AutoResponseApiEventHttpResponseMapper(new OwinContextResolver()));
        }
    }
}