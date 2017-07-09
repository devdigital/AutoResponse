namespace AutoResponse.Data.Exceptions
{
    using System;

    public class ServiceErrorException : Exception
    {
        public ServiceErrorException(string message)
            : base(message)
        {            
        }

        public ServiceErrorException(Exception exception) 
            : base("There was an unhandled exception", exception)
        {
        }
    }
}