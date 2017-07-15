namespace AutoResponse.Core.Exceptions
{
    using System;

    public class ServiceErrorException : Exception
    {
        public ServiceErrorException(string message)
            : base(message)
        {            
        }
    }
}