namespace AutoResponse.WebApi2.Logging
{
    using System;
    using System.Web.Http.ExceptionHandling;

    public class DefaultExceptionLogger : ExceptionLogger
    {
        private readonly ILogger<DefaultExceptionLogger> logger;

        public DefaultExceptionLogger(ILoggerFactory loggerFactory)
        {
            //if (loggerFactory == null)
            //{
            //    throw new ArgumentNullException(nameof(loggerFactory));
            //}

            // this.logger = loggerFactory.Create<DefaultExceptionLogger>();
        }

        public override bool ShouldLog(ExceptionLoggerContext context)
        {
            return true;
        }

        public override void Log(ExceptionLoggerContext context)
        {
            // this.logger.Error(context.Exception, "There was an unhandled exception");
        }
    }
}
