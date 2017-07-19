namespace AutoResponse.Core.Mappers
{
    public interface IHttpResponseFormatter
    {
        string EntityMessageToResourceMessage(string message);

        string EntityTypeToResource(string entityType);

        string EntityPropertyToField(string entityProperty);
    }
}