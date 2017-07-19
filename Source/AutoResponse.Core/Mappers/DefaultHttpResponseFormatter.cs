namespace AutoResponse.Core.Mappers
{
    using Humanizer;

    internal class DefaultHttpResponseFormatter : IHttpResponseFormatter
    {
        public string EntityMessageToResourceMessage(string message)
        {
            return string.IsNullOrWhiteSpace(message) ? null : message;
        }

        public string EntityTypeToResource(string entityType)
        {
            if (string.IsNullOrWhiteSpace(entityType))
            {
                return null;
            }

            return entityType.Kebaberize();
        }

        public string EntityPropertyToField(string entityProperty)
        {
            if (string.IsNullOrWhiteSpace(entityProperty))
            {
                return null;
            }

            return entityProperty.Kebaberize();
        }
    }
}