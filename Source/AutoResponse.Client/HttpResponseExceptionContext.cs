namespace AutoResponse.Client
{
    using System;

    public class HttpResponseExceptionContext
    {
        public HttpResponseExceptionContext(IAutoResponseHttpResponseFormatter formatter)
        {
            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            this.Formatter = formatter;
        }

        public IAutoResponseHttpResponseFormatter Formatter { get; }
    }
}