namespace AutoResponse.Client
{
    using System;
    using System.Net.Http;

    public static class HttpResponseMessageExtensions
    {
        public static void HandleErrors(this HttpResponseMessage response)
        {
            HandleErrors(response, new AutoResponseHttpResponseExceptionMapper());
        }

        public static void HandleErrors(this HttpResponseMessage response, IHttpResponseExceptionMapper mapper)
        {
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            if (mapper.IsErrorResponse(response))
            {
                var exception = mapper.GetException(response);
                if (exception != null)
                {
                    throw exception;
                }

                var defaultException = mapper.GetDefaultException(response);
                if (defaultException != null)
                {
                    throw defaultException;
                }
            }
        }
    }
}
