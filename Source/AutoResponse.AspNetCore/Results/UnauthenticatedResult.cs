using AutoResponse.Core.ApiEvents;

namespace AutoResponse.AspNetCore.Results
{
    public class UnauthenticatedResult : AutoResponseResult
    {
        public UnauthenticatedResult(string message)
            : base(new UnauthenticatedApiEvent(message))
        {
        }
    }
}