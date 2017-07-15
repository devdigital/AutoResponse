namespace AutoResponse.Core.Exceptions
{
    using System;

    public class UnauthenticatedException : Exception
    {
        public UnauthenticatedException(string message)
            : base(message)
        {            
        }
    }
}
