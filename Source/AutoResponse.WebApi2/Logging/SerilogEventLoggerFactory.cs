namespace AutoResponse.WebApi2.Logging
{
    using System;

    using ApiTalk.Logging;

    public class SerilogEventLoggerFactory : IEventLoggerFactory
    {
        private readonly LoggingSettings loggingSettings;

        public SerilogEventLoggerFactory(LoggingSettings loggingSettings)
        {
            if (loggingSettings == null)
            {
                throw new ArgumentNullException(nameof(loggingSettings));
            }

            this.loggingSettings = loggingSettings;
        }

        public IEventLogger<TContext> Create<TContext>()
        {
            return new SerilogEventLogger<TContext>(
                new SerilogLogger<TContext>(this.loggingSettings));
        }
    }
}