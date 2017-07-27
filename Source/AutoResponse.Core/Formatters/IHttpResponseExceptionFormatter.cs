namespace AutoResponse.Core.Formatters
{
    using AutoResponse.Core.ApiEvents;

    public interface IHttpResponseExceptionFormatter
    {
        string Message(string message);

        string Resource(string entityType);

        string Field(string entityProperty);

        string ApiEventToCode(AutoResponseApiEvent apiEvent);
    }
}