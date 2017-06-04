namespace ApiTalk.Logging
{
    using System;

    using AutoResponse.WebApi2.Logging;

    public class SerilogLoggerFactory : ILoggerFactory
    {
        private readonly LoggingSettings settings;

        public SerilogLoggerFactory(LoggingSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            this.settings = settings;
        }

        public ILogger<TContext> Create<TContext>()
        {
            return new SerilogLogger<TContext>(this.settings);
        }
    }
}
