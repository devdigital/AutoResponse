namespace AutoResponse.WebApi2.ExceptionHandling
{
    using System;
    using System.Web.Http.ExceptionHandling;

    public class AutoResponseExceptionHandler : ExceptionHandler
    {
        private readonly IExceptionActionResultMapper actionResultMapper;

        public AutoResponseExceptionHandler()
        {
            this.actionResultMapper = new AutoResponseExceptionActionResultMapper();
        }

        public AutoResponseExceptionHandler(IExceptionActionResultMapper actionResultMapper)
        {
            if (actionResultMapper == null)
            {
                throw new ArgumentNullException(nameof(actionResultMapper));
            }

            this.actionResultMapper = actionResultMapper;
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

            var actionResult = this.actionResultMapper.Get(context.Request, context.Exception);
            if (actionResult == null)
            {
                base.Handle(context);
                return;
            }

            context.Result = actionResult;
        }
    }
}