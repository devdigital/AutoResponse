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

        public ServiceErrorException(string code, Exception exception)
            : base(exception.Message, new ServiceErrorApiEvent(code, exception))
        {            
        }

        public ServiceErrorException(string message)
            : base(message, new ServiceErrorApiEvent(message))
        {            
        }

        public ServiceErrorException(Exception exception) 
            : base(exception.Message, new ServiceErrorApiEvent(exception))
        {            
        }
    }
}