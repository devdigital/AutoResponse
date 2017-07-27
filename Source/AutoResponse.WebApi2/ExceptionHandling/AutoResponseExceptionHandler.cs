namespace AutoResponse.WebApi2.ExceptionHandling
{
    using System.Web.Http.ExceptionHandling;

    using AutoResponse.Core.ApiEvents;
    using AutoResponse.Core.Exceptions;
    using AutoResponse.WebApi2.Results;

    public class AutoResponseExceptionHandler : ExceptionHandler
    {
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

            object apiEvent = null;
            var autoResponseException = context.Exception as AutoResponseException;
            if (autoResponseException != null)
            {
                apiEvent = autoResponseException.Event;                
            }

            if (apiEvent == null)
            {
                apiEvent = context.Exception?.GetType().GetProperty("Event")
                    ?.GetValue(context.Exception, null);
            }

            if (apiEvent == null)
            {
                apiEvent = new ServiceErrorApiEvent(context.Exception);
            }
             
            context.Result = new AutoResponseResult(context.Request, apiEvent);
            base.Handle(context);
        }
    }
}