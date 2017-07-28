namespace AutoResponse.Client
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public static class HttpResponseMessageExtensions
    {
        public static async Task HandleErrors(this HttpResponseMessage response, bool throwOnUnhandledResponses = true)
        {
            await HandleErrors(response, new AutoResponseHttpResponseExceptionMapper(), throwOnUnhandledResponses);
        }

        public static async Task HandleErrors(this HttpResponseMessage response, IHttpResponseExceptionMapper mapper, bool throwOnUnhandledResponses = true)
        {
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            var isErrorResponse = await mapper.IsErrorResponse(response);
            if (isErrorResponse)
            {
                var exception = await mapper.GetException(response);
                if (exception != null)
                {
                    throw exception;
                }

                if (throwOnUnhandledResponses)
                {
                    // TODO: better exception information
                    throw new Exception(
                        $"There was an HTTP error with status code {response.StatusCode}");
                } 
            }
        }
    }
}
