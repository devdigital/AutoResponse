namespace AutoResponse.WebApi2.ExceptionHandling
{
    using System;
    using System.Web.Http.ExceptionHandling;

    using AutoResponse.Core;
    using AutoResponse.Core.Mappers;
    using AutoResponse.WebApi2.Results;

    public class AutoResponseExceptionHandler : ExceptionHandler
    {
        private readonly IExceptionHttpResponseMapper mapper;

        public AutoResponseExceptionHandler()
        {
            this.mapper = new AutoResponseExceptionHttpResponseMapper();
        }

        public AutoResponseExceptionHandler(IExceptionHttpResponseMapper mapper)
        {
            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            this.mapper = mapper;
        }

        // Fix for exception handler not being invoked because of CORs package handling exceptions 
        // See http://stackoverflow.com/a/24634485/248164
        public override bool ShouldHandle(ExceptionHandlerContext context)
        {
            return true;
        }

        public override void Handle(ExceptionHandlerContext context)
        {
            if (context?.Exception == null)
            {
                base.Handle(context);
                return;
            }

            var httpResponse = this.mapper.GetHttpResponse(context.Exception);
            if (httpResponse == null)
            {
                base.Handle(context);
                return;
            }

            context.Result = new HttpResponseResult(context.Request, httpResponse);
        }
    }
}