namespace AutoResponse.Core.Exceptions
{
    using System;

    using AutoResponse.Core.ApiEvents;

    public class ServiceErrorException : AutoResponseException
    {
        public ServiceErrorException(string code, string message)
            : base(new ServiceErrorApiEvent(code, message))
        {            
        }

        public ServiceErrorException(string code, Exception exception)
            : base(new ServiceErrorApiEvent(code, exception))
        {            
        }

        public ServiceErrorException(string message)
            : base(new ServiceErrorApiEvent(message))
        {            
        }

        public ServiceErrorException(Exception exception) 
            : base(new ServiceErrorApiEvent(exception))
        {            
        }
    }
}