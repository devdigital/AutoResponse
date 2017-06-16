namespace AutoResponse.WebApi2.Instrumentation
{
    using System;

    public class CallLoggingStartingEvent : IHaveStructuredLog
    {        
        public CallLoggingStartingEvent(DateTime time, Type declaringType, string methodName, object[] parameters)
        {
            if (declaringType == null)
            {
                throw new ArgumentNullException(nameof(declaringType));
            }   
                     
            if (string.IsNullOrWhiteSpace(methodName))
            {
                throw new ArgumentNullException(nameof(methodName));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            this.Time = time;
            this.DeclaringType = declaringType;
            this.MethodName = methodName;
            this.Parameters = parameters;
        }

        public DateTime Time { get; }

        public Type DeclaringType { get; }

        public string MethodName { get; }

        public object[] Parameters { get; }

        public StructuredMessage GetStructuredMessage()
        {
            return new StructuredMessage(
                "Method {DeclaringType}.{MethodName} with parameters {@Parameters} invoked at {StartTime}",
                this.DeclaringType,
                this.MethodName,
                this.Parameters,
                this.Time);
        }
    }
}