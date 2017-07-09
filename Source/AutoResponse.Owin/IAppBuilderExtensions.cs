namespace AutoResponse.Owin
{
    using AutoResponse.Core.Mappers;
    using AutoResponse.WebApi2.ExceptionHandling;

    using global::Owin;

    public static class IAppBuilderExtensions
    {
        public static void UseAutoResponse(this IAppBuilder appBuilder, IExceptionHttpResponseMapper mapper = null)
        {
            appBuilder.Use<AutoResponseExceptionMiddleware>(
                mapper ?? new AutoResponseExceptionHttpResponseMapper());
        }
    }
}