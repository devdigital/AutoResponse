namespace AutoResponse.Client
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public static class HttpResponseMessageExtensions
    {
        public static async Task HandleErrors(this HttpResponseMessage response)
        {
            await HandleErrors(response, new AutoResponseHttpResponseExceptionMapper(), new NullUnhandledResponseHandler());
        }
        public static async Task HandleErrors(this HttpResponseMessage response, IUnhandledResponseHandler unhandledResponseHandler)
        {
            await HandleErrors(response, new AutoResponseHttpResponseExceptionMapper(), unhandledResponseHandler);
        }

        public static async Task HandleErrors(this HttpResponseMessage response, IHttpResponseExceptionMapper mapper)
        {
            await HandleErrors(response, mapper, new NullUnhandledResponseHandler());
        }

        public static async Task HandleErrors(this HttpResponseMessage response, IHttpResponseExceptionMapper mapper, IUnhandledResponseHandler unhandledResponseHandler)
        {
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            if (unhandledResponseHandler == null)
            {
                throw new ArgumentNullException(nameof(unhandledResponseHandler));
            }

            var isErrorResponse = await mapper.IsErrorResponse(response);
            if (isErrorResponse)
            {
                var exception = await mapper.GetException(response);
                if (exception != null)
                {
                    throw exception;
                }

                await unhandledResponseHandler.Handle(
                    new ResponseContent(response));
            }
        }
    }
}
