namespace AutoResponse.WebApi2.Logging
{
    public class NullLoggerFactory : ILoggerFactory
    {
        public ILogger<TContext> Create<TContext>()
        {
            return new NullLogger<TContext>();
        }
    }
}