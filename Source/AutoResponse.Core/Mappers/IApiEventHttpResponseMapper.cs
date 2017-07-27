namespace AutoResponse.Core.Mappers
{
    using AutoResponse.Core.Responses;

    public interface IApiEventHttpResponseMapper
    {
        IHttpResponse GetHttpResponse(
            object context,
            object apiEvent);
    }
}