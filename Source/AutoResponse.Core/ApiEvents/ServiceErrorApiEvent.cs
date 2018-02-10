namespace AutoResponse.Core.ApiEvents
{
    using System;

    public class ServiceErrorApiEvent : IAutoResponseApiEvent
    {
        public ServiceErrorApiEvent(string code, string message)
        {
            if (string.IsNullOrWhiteSpace(nameof(message)))
            {
                throw new ArgumentNullException(nameof(message));
            }

            this.Code = code;
            this.Message = message;
        }

        public ServiceErrorApiEvent(string code, Exception exception)
        {
            this.Code = code;
            this.Exception = exception ?? throw new ArgumentNullException(nameof(exception));
        }

        public ServiceErrorApiEvent(string message) : this("AR500", message)
        {            
        }

        public ServiceErrorApiEvent(Exception exception) : this("AR500", exception)
        {
        }

        public string Code { get; }

        public string Message { get; }

        public Exception Exception { get; }        
    }
}