namespace AutoResponse.WebApi2.Logging
{
    public interface ILoggerFactory
    {
        ILogger<TContext> Create<TContext>();
    }
}