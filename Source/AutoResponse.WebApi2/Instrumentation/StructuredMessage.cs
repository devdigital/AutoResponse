namespace AutoResponse.WebApi2.Instrumentation
{
    using System;

    public class StructuredMessage
    {
        public StructuredMessage(string messageTemplate, params object[] propertyValues)
        {
            if (string.IsNullOrWhiteSpace(messageTemplate))
            {
                throw new ArgumentNullException(nameof(messageTemplate));
            }

            if (propertyValues == null)
            {
                throw new ArgumentNullException(nameof(propertyValues));
            }

            this.MessageTemplate = messageTemplate;
            this.PropertyValues = propertyValues;
        }
        
        public string MessageTemplate { get; }

        public object[] PropertyValues { get; }
    }
}