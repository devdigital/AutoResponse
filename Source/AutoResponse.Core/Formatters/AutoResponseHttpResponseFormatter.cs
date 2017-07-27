namespace AutoResponse.Core.Formatters
{
    using System;

    using AutoResponse.Core.ApiEvents;

    using Humanizer;

    internal class AutoResponseHttpResponseFormatter : IHttpResponseFormatter
    {
        private readonly string[] postFixes;

        private readonly AutoResponseApiEventCodeMapper apiEventCodeMapper;

        public AutoResponseHttpResponseFormatter()
        {
            this.postFixes = new[] { "ApiModel", "Dto" };
            this.apiEventCodeMapper = new AutoResponseApiEventCodeMapper();
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

        public string ApiEventToCode(AutoResponseApiEvent apiEvent)
        {
            return this.apiEventCodeMapper.GetCode(apiEvent);
        }
    }
}