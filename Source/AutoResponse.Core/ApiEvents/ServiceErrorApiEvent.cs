namespace AutoResponse.Core.ApiEvents
{
    using System;

    public class ServiceErrorApiEvent : AutoResponseApiEvent
    {
        public ServiceErrorApiEvent(Exception exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            this.Exception = exception;
        }

        public Exception Exception { get; }        
    }
}