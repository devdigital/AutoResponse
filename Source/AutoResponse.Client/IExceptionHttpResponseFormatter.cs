namespace AutoResponse.Client
{
    public interface IExceptionHttpResponseFormatter
    {
        string Message(string message);

        string EntityType(string resource);

        string EntityProperty(string field);
    }
}