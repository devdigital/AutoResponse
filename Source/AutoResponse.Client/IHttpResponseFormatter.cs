namespace AutoResponse.Client
{
    public interface IHttpResponseFormatter
    {
        string Message(string message);

        string EntityType(string resource);

        string EntityProperty(string field);

        string Code(string code);
    }
}