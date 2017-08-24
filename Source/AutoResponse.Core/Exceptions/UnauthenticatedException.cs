namespace AutoResponse.Core.Exceptions
{
    using AutoResponse.Core.ApiEvents;

    public class UnauthenticatedException : AutoResponseException<UnauthenticatedApiEvent>
    {
        public UnauthenticatedException(string code, string message)
            : base(new UnauthenticatedApiEvent(code, message))
        {            
        }

        public UnauthenticatedException(string message)
            : base(new UnauthenticatedApiEvent(message))
        {
        }
    }
}
