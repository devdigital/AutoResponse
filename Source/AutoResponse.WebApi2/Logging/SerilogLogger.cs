namespace AutoResponse.WebApi2.Logging
{
    using System;

    public class SerilogLogger<TContext> : ILogger<TContext>
    {
        private readonly ILogger logger;

        public SerilogLogger(LoggingSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var configuration = new LoggerConfiguration()
                .MinimumLevel(settings.Level);

            if (!string.IsNullOrWhiteSpace(settings.LogFilePath))
            {
                configuration = configuration.WriteTo.RollingFile(settings.LogFilePath);
            }

            if (!string.IsNullOrWhiteSpace(settings.SeqUri))
            {
                configuration = configuration.WriteTo.Seq(settings.SeqUri);
            }
                
            this.logger = configuration                
                .CreateLogger()
                .ForContext<TContext>();
        }        

        public void Trace(string messageTemplate, params object[] propertyValues)
        {
            this.logger.Verbose(exception: null, messageTemplate: messageTemplate, propertyValues: propertyValues);
        }

        public void Debug(string messageTemplate, params object[] propertyValues)
        {
            this.logger.Debug(exception: null, messageTemplate: messageTemplate, propertyValues: propertyValues);
        }

        public void Information(string messageTemplate, params object[] propertyValues)
        {
            this.logger.Information(exception: null, messageTemplate: messageTemplate, propertyValues: propertyValues);
        }

        public void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            this.logger.Warning(exception, messageTemplate, propertyValues);
        }

        public void Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            this.logger.Error(exception, messageTemplate, propertyValues);
        }
    }
}