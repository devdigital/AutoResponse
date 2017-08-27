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

        public ServiceErrorApiEvent(string code, string message, Exception exception)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentNullException(nameof(code));
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            this.Code = code;
            this.Message = message;
            this.Exception = exception;
        }

        public ServiceErrorApiEvent(string message) : this("AR500", message)
        {            
        }

        public ServiceErrorApiEvent(string message, Exception exception) : this("AR500", message, exception)
        {
        }

        public string Code { get; }

        public string Message { get; }

        public Exception Exception { get; }
    }
}