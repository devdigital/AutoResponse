namespace AutoResponse.WebApi2.Instrumentation
{
    using System;
    using System.Diagnostics;

    using ApiTalk.Instrumentation;

    public class TimingInterceptor : IInterceptor
    {
        private readonly IEventLogger<TimingInterceptor> logger;

        public TimingInterceptor(IEventLoggerFactory loggerFactory)
        {
            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }      

            this.logger = loggerFactory.Create<TimingInterceptor>();
        }

        public void Intercept(IInvocation invocation)
        {
            // Ignore get/set properties (also ignores operator overloads)
            if (invocation.Method.IsSpecialName)
            {
                invocation.Proceed();
                return;
            }

            this.logger.Trace(new CallLoggingStartingEvent(
                DateTime.Now, 
                invocation.TargetType,
                invocation.Method.Name, 
                invocation.Arguments));

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            invocation.Proceed();

            stopWatch.Stop();
            this.logger.Trace(new CallLoggingCompleteEvent(
                DateTime.Now, 
                invocation.TargetType,
                invocation.Method.Name, 
                stopWatch.Elapsed, 
                invocation.ReturnValue));
        }
    }
}