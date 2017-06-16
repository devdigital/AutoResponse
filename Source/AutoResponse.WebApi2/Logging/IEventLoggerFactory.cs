namespace AutoResponse.WebApi2.Logging
{
    public interface IEventLoggerFactory
    {
        IEventLogger<TContext> Create<TContext>();
    }
}