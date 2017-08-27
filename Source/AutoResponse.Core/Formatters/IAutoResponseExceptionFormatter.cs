namespace AutoResponse.Core.Formatters
{
    public interface IAutoResponseExceptionFormatter
    {
        string Message(string message);

        string Resource(string entityType);

        string Field(string entityProperty);

        string Code(string code);
    }
}