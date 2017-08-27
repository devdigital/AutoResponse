using System.Threading.Tasks;

namespace AutoResponse.Core.Mappers
{
    using AutoResponse.Core.Responses;

    public interface IApiEventHttpResponseMapper
    {
        Task<IHttpResponse> GetHttpResponse(
            object context,
            object apiEvent);
    }
}