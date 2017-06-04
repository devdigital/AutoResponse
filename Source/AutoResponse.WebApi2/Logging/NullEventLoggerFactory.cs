namespace AutoResponse.WebApi2.Logging
{
    public class NullEventLoggerFactory : IEventLoggerFactory
    {
        public IEventLogger<TContext> Create<TContext>()
        {
            return new NullEventLogger<TContext>();
        }
    }
}