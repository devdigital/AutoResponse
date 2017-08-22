namespace AutoResponse.Core.Mappers
{
    using System;

    using AutoResponse.Core.Formatters;

    public class ExceptionHttpResponseContext
    {
        public ExceptionHttpResponseContext(object context, IExceptionFormatter formatter)
        {
            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            this.Context = context;
            this.Formatter = formatter;
        }        

        public object Context { get; }

        public IExceptionFormatter Formatter { get; }
    }
}