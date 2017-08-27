namespace AutoResponse.Client
{
    using Humanizer;

    public class AutoResponseHttpResponseFormatter : IAutoResponseHttpResponseFormatter
    {
        private readonly bool useCamelCase;

        public AutoResponseHttpResponseFormatter(bool useCamelCase = true)
        {
            this.useCamelCase = useCamelCase;
        }

        public string Message(string message)
        {
            return string.IsNullOrWhiteSpace(message) ? null : message;
        }

        public string EntityType(string resource)
        {
            return string.IsNullOrWhiteSpace(resource) 
                ? null 
                : this.ConvertCase(resource);
        }

        public string EntityProperty(string field)
        {
            return string.IsNullOrWhiteSpace(field)
                ? null
                : this.ConvertCase(field);
        }

        public string Code(string code)
        {
            return string.IsNullOrWhiteSpace(code) ? null : code;
        }

        private string ConvertCase(string value)
        {
            // If not camel case, assume kebab case
            return this.useCamelCase 
                ? value?.Pascalize() 
                : value?.Underscore()?.Pascalize();
        }
    }
}