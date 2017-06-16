namespace AutoResponse.WebApi2.Instrumentation
{
    using System;
    using System.Threading.Tasks;

    public class CallLoggingCompleteEvent : IHaveStructuredLog
    {
        public CallLoggingCompleteEvent(DateTime time, Type declaringType, string methodName, TimeSpan elapsedTime, object returnValue)
        {
            if (declaringType == null)
            {
                throw new ArgumentNullException(nameof(declaringType));
            }

            if (string.IsNullOrWhiteSpace(methodName))
            {
                throw new ArgumentNullException(nameof(methodName));
            }            

            this.Time = time;
            this.DeclaringType = declaringType;
            this.MethodName = methodName;
            this.ElapsedTime = elapsedTime;
            this.ReturnValue = returnValue;
        }

        public DateTime Time { get; }        

        public TimeSpan ElapsedTime { get; }

        public object ReturnValue { get; }

        public Type DeclaringType { get; }

        public string MethodName { get; }

        public StructuredMessage GetStructuredMessage()
        {
            return new StructuredMessage(
                "Method {DeclaringType}.{MethodName} finished invocation at {Time} and returned with value {@MethodReturnValue}. Time elapsed within method was {ElapsedTime}",
                this.DeclaringType,
                this.MethodName,
                this.Time,
                this.GetReturnValue(this.ReturnValue),
                this.ElapsedTime);
        }

        private object GetReturnValue(object returnValue)
        {
            if (returnValue == null)
            {
                return null;
            }

            var returnValueType = returnValue.GetType();
            if (returnValueType.IsGenericType && returnValueType.GetGenericTypeDefinition() == typeof(Task<>))
            {
                var resultType = returnValueType.GetGenericArguments()[0];
                var convertMethod = this.GetType().GetMethod("RunTask").MakeGenericMethod(resultType);
                var result = convertMethod.Invoke(null, new[] { returnValue });
                return result;
            }

            return returnValue;
        }

        public static T RunTask<T>(Task<T> task)
        {
            return task.Result;            
        }
    }
}