using System.Threading.Tasks;

namespace AutoResponse.Client
{
    public interface IUnhandledResponseHandler
    {
        Task Handle(ResponseContent response);
    }
}