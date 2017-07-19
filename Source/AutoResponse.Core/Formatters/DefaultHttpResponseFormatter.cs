namespace AutoResponse.Core.Formatters
{
    using System;

    using AutoResponse.Core.Mappers;

    using Humanizer;

    internal class DefaultHttpResponseFormatter : IHttpResponseFormatter
    {
        private readonly string[] postFixes;

        public DefaultHttpResponseFormatter()
        {
            this.postFixes = new[] { "ApiModel", "Dto" };
        }

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

        public string EntityPropertyToField(string entityProperty)
        {
            return string.IsNullOrWhiteSpace(entityProperty) 
                ? null : entityProperty.Kebaberize();
        }
    }
}