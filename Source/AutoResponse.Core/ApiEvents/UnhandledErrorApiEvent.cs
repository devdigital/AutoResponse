using System;

namespace AutoResponse.Core.ApiEvents
{
    public class UnhandledErrorApiEvent : IAutoResponseApiEvent
    { 
        public UnhandledErrorApiEvent(string code, Exception exception)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentNullException(nameof(code));
            }

            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            this.Code = code;
            this.Exception = exception;
        }

        public UnhandledErrorApiEvent(Exception exception) : this("AR500", exception)
        {            
        }
        
        public string Code { get; }

        public Exception Exception { get; }
    }
}