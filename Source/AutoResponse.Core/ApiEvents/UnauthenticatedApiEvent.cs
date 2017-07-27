namespace AutoResponse.Core.ApiEvents
{
    using System;

    public class UnauthenticatedApiEvent
    {
        public UnauthenticatedApiEvent()
        {            
        }

        public UnauthenticatedApiEvent(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            this.Message = message;
        }

        public string Message { get; }
    }
}