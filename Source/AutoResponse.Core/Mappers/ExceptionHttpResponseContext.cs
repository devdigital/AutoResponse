namespace AutoResponse.Core.Mappers
{
    using System;

    using AutoResponse.Core.Formatters;

    public class ExceptionHttpResponseContext
    {
        public ExceptionHttpResponseContext(object context, IAutoResponseExceptionFormatter formatter)
        {
            this.Context = context;
            this.Formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
        }        

        public object Context { get; }

        public IAutoResponseExceptionFormatter Formatter { get; }
    }
}