namespace AutoResponse.Client
{
    using System;

    public class HttpResponseExceptionContext
    {
        public HttpResponseExceptionContext(IHttpResponseFormatter formatter)
        {
            this.Formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
        }

        public IHttpResponseFormatter Formatter { get; }
    }
}