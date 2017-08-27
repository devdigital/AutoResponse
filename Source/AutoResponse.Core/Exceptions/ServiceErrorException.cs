namespace AutoResponse.Core.Exceptions
{
    using System;

    using AutoResponse.Core.ApiEvents;

    public class ServiceErrorException : AutoResponseException<ServiceErrorApiEvent>
    {
        public ServiceErrorException(string code, string message)
            : base(message, new ServiceErrorApiEvent(code, message))
        {            
        }

        public ServiceErrorException(string code, string message, Exception exception)
            : base(message, new ServiceErrorApiEvent(code, message, exception))
        {            
        }

        public ServiceErrorException(string message)
            : base(message, new ServiceErrorApiEvent(message))
        {            
        }

        public ServiceErrorException(string message, Exception exception) 
            : base(message, new ServiceErrorApiEvent(message, exception))
        {            
        }
    }
}