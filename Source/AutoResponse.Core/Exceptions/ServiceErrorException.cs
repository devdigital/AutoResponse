namespace AutoResponse.Core.Exceptions
{
    using System;

    using AutoResponse.Core.ApiEvents;

    public class ServiceErrorException : AutoResponseException
    {
        public ServiceErrorException(Exception exception) 
            : base(new ServiceErrorApiEvent(exception))
        {            
        }
    }
}