namespace ApiTalk.Logging
{
    using System;
    using System.Linq;

    using ApiTalk.Instrumentation;

    using AutoResponse.WebApi2.Logging;

    public class SerilogEventLogger<TContext> : IEventLogger<TContext>
    {
        private readonly SerilogLogger<TContext> logger;

        public SerilogEventLogger(SerilogLogger<TContext> logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            this.logger = logger;            
        }

        public void Trace<TEvent>(TEvent @event)
        {
            var structuredMessage = GetStructuredMessage(@event);
            this.logger.Trace(structuredMessage.MessageTemplate, structuredMessage.PropertyValues);
        }

        public void Debug<TEvent>(TEvent @event)
        {
            var structuredMessage = GetStructuredMessage(@event);
            this.logger.Debug(structuredMessage.MessageTemplate, structuredMessage.PropertyValues);
        }

        public void Information<TEvent>(TEvent @event)
        {
            var structuredMessage = GetStructuredMessage(@event);
            this.logger.Information(structuredMessage.MessageTemplate, structuredMessage.PropertyValues);
        }

        public void Warning<TEvent>(Exception exception, TEvent @event)
        {
            var structuredMessage = GetStructuredMessage(@event);
            this.logger.Warning(exception, structuredMessage.MessageTemplate, structuredMessage.PropertyValues);
        }

        public void Error<TEvent>(Exception exception, TEvent @event)
        {
            var structuredMessage = GetStructuredMessage(@event);
            this.logger.Error(exception, structuredMessage.MessageTemplate, structuredMessage.PropertyValues);            
        }

        private static StructuredMessage GetStructuredMessage<TEvent>(TEvent @event)
        {
            var structuredEvent = @event as IHaveStructuredLog;
            if (structuredEvent == null)
            {
                return new StructuredMessage("{EventName} {@EventProperties}", @event.GetType().Name, @event);
            }

            var structuredMessage = structuredEvent.GetStructuredMessage();
            var parameters = new[] { @event.GetType().Name }.Concat(structuredMessage.PropertyValues).ToArray();
            return new StructuredMessage($"{{EventName}} {structuredMessage.MessageTemplate}", parameters);
        }        
    }
}