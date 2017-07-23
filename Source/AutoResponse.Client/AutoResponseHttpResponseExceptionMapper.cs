namespace AutoResponse.Client
{
    using System;
    using System.Net;
    using System.Net.Http;

    public class AutoResponseHttpResponseExceptionMapper : IHttpResponseExceptionMapper
    {
        public bool IsErrorResponse(HttpResponseMessage response)
        {
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            return response.StatusCode >= (HttpStatusCode)400;
        }

        public Exception GetException(HttpResponseMessage response)
        {
            throw new NotImplementedException();
        }

        public Exception GetDefaultException(HttpResponseMessage response)
        {
            throw new NotImplementedException();
        }
    }
}