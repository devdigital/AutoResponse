namespace AutoResponse.Owin
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.Owin;

    public class AutoResponseExceptionMiddleware : OwinMiddleware
    {
        public AutoResponseExceptionMiddleware(OwinMiddleware next)
            : base(next)
        {  
        }

        public override async Task Invoke(IOwinContext context)
        {
            var response = context.Response;

            response.OnSendingHeaders(state =>
            {
                var resp = (OwinResponse)state;
                resp.Headers.Clear();
                resp.StatusCode = 403;
                resp.ReasonPhrase = "Forbidden";
            }, response);

            try
            {
                await this.Next.Invoke(context);
            }
            catch (Exception ex)
            {                
                using (var writer = new StreamWriter(context.Response.Body))
                {
                    await writer.WriteAsync("This is the body");
                }
            }
        }
    }
}
