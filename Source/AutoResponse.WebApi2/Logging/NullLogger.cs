namespace AutoResponse.WebApi2.Logging
{
    using System;

    public class NullLogger<TContext> : ILogger<TContext>
    {
        public void Trace(string messageTemplate, params object[] propertyValues)
        {            
        }

        public void Debug(string messageTemplate, params object[] propertyValues)
        {
        }

        public void Information(string messageTemplate, params object[] propertyValues)
        {
        }

        public void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
        {
        }

        public void Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
        }
    }
}