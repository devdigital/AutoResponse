namespace AutoResponse.Core.Exceptions
{
    using AutoResponse.Core.ApiEvents;

    public class UnauthenticatedException : AutoResponseException
    {
        public UnauthenticatedException() 
            : base(new UnauthenticatedApiEvent())
        {            
        }

        public UnauthenticatedException(string message) 
            : base(new UnauthenticatedApiEvent(message))
        {            
        }
    }
}
