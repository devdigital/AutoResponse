namespace AutoResponse.Core.Formatters
{
    using System;

    using Humanizer;

    internal class AutoResponseExceptionHttpResponseFormatter : IHttpResponseExceptionFormatter
    {
        private readonly string[] postFixes;

        private readonly IApiEventCodeMapper apiEventCodeMapper;

        public AutoResponseExceptionHttpResponseFormatter()
        {
            this.postFixes = new[] { "ApiModel", "Dto" };
            this.apiEventCodeMapper = new AutoResponseApiEventCodeMapper();
        }

        public AutoResponseExceptionHttpResponseFormatter(IApiEventCodeMapper apiEventCodeMapper)
        {
            if (apiEventCodeMapper == null)
            {
                throw new ArgumentNullException(nameof(apiEventCodeMapper));
            }

            this.apiEventCodeMapper = apiEventCodeMapper;
        }

        public string Message(string message)
        {
            return string.IsNullOrWhiteSpace(message) ? null : message;
        }

        public string Resource(string entityType)
        {
            if (string.IsNullOrWhiteSpace(entityType))
            {
                return null;
            }

            foreach (var postFix in this.postFixes)
            {
                if (!entityType.EndsWith(postFix))
                {
                    continue;
                }

                if (string.Compare(entityType, postFix, StringComparison.Ordinal) != 0)
                {
                    entityType = entityType.Substring(0, entityType.Length - postFix.Length);
                }
                
                break;
            }

            return string.IsNullOrWhiteSpace(entityType) ? null : entityType.Kebaberize();
        }

        public string Field(string entityProperty)
        {
            return string.IsNullOrWhiteSpace(entityProperty) 
                ? null : entityProperty.Kebaberize();
        }

        public string ApiEventToCode(object apiEvent)
        {
            return this.apiEventCodeMapper.GetCode(apiEvent);
        }
    }
}