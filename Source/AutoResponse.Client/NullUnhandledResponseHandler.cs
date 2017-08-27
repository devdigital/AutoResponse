using System.Threading.Tasks;

namespace AutoResponse.Client
{
    public class NullUnhandledResponseHandler : IUnhandledResponseHandler
    {
        public Task Handle(ResponseContent response)
        {
            return Task.FromResult(0);
        }
    }
}