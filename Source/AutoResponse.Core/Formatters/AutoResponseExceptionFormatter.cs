namespace AutoResponse.Core.Formatters
{
    using System;

    using Humanizer;

    internal class AutoResponseExceptionFormatter : IExceptionFormatter
    {
        private readonly string[] postFixes;

        private readonly IApiEventCodeFormatter apiEventCodeFormatter;

        public AutoResponseExceptionFormatter()
        {
            this.postFixes = new[] { "ApiModel", "Dto" };
            this.apiEventCodeFormatter = new NullApiEventCodeFormatter();
        }

        public AutoResponseExceptionFormatter(IApiEventCodeFormatter apiEventCodeFormatter)
        {
            if (apiEventCodeFormatter == null)
            {
                throw new ArgumentNullException(nameof(apiEventCodeFormatter));
            }

            this.apiEventCodeFormatter = apiEventCodeFormatter;
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

        public string Code(string code)
        {
            return this.apiEventCodeFormatter.Format(code);
        }
    }
}