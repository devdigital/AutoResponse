namespace AutoResponse.Core.Formatters
{
    public interface IHttpResponseExceptionFormatter
    {
        string Message(string message);

        string Resource(string entityType);

        string Field(string entityProperty);

        string ApiEventToCode(object apiEvent);
    }
}