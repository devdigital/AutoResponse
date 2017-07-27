namespace AutoResponse.Client
{
    using Humanizer;

    public class AutoResponseExceptionHttpResponseFormatter : IExceptionHttpResponseFormatter
    {
        public string Message(string message)
        {
            return string.IsNullOrWhiteSpace(message) ? null : message;
        }

        public string EntityType(string resource)
        {
            return string.IsNullOrWhiteSpace(resource) 
                ? null 
                : resource.Underscore().Pascalize();
        }

        public string EntityProperty(string field)
        {
            return string.IsNullOrWhiteSpace(field)
                ? null
                : field.Underscore().Pascalize();
        }
    }
}