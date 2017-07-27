namespace AutoResponse.Client
{
    using System;

    public class HttpResponseExceptionContext
    {
        public HttpResponseExceptionContext(IExceptionHttpResponseFormatter formatter)
        {
            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            this.Formatter = formatter;
        }

        public IExceptionHttpResponseFormatter Formatter { get; }
    }
}