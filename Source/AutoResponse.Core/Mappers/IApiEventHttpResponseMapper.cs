namespace AutoResponse.Core.Mappers
{
    using AutoResponse.Core.ApiEvents;
    using AutoResponse.Core.Responses;

    public interface IApiEventHttpResponseMapper
    {
        IHttpResponse GetHttpResponse(
            object context,
            AutoResponseApiEvent apiEvent);
    }
}