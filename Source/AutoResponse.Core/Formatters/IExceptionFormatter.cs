namespace AutoResponse.Core.Formatters
{
    public interface IExceptionFormatter
    {
        string Message(string message);

        string Resource(string entityType);

        string Field(string entityProperty);

        string Code(string code);
    }
}