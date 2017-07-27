namespace AutoResponse.Core.Mappers
{
    using System;

    using AutoResponse.Core.Formatters;

    public class ExceptionHttpResponseContext
    {
        public ExceptionHttpResponseContext(object context, IHttpResponseExceptionFormatter formatter)
        {
            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            this.Context = context;
            this.Formatter = formatter;
        }        

        public object Context { get; }

        public IHttpResponseExceptionFormatter Formatter { get; }
    }
}