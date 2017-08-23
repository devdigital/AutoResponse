namespace AutoResponse.Core.Formatters
{
    using System;

    using Humanizer;

    public class AutoResponseExceptionFormatter : IAutoResponseExceptionFormatter
    {
        private readonly string[] postFixes;

        private readonly IAutoResponseCodeFormatter autoResponseCodeFormatter;

        private readonly bool useCamelCase;

        public AutoResponseExceptionFormatter(bool useCamelCase = true)
        {
            this.postFixes = new[] { "ApiModel", "Dto" };
            this.autoResponseCodeFormatter = new NullAutoResponseCodeFormatter();
            this.useCamelCase = useCamelCase;
        }

        public AutoResponseExceptionFormatter(IAutoResponseCodeFormatter autoResponseCodeFormatter, bool useCamelCase = true)
        {
            if (autoResponseCodeFormatter == null)
            {
                throw new ArgumentNullException(nameof(autoResponseCodeFormatter));
            }

            this.autoResponseCodeFormatter = autoResponseCodeFormatter;
            this.useCamelCase = useCamelCase;
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

            return string.IsNullOrWhiteSpace(entityType) ? null : ConvertCase(entityType);
        }

        public string Field(string entityProperty)
        {
            return string.IsNullOrWhiteSpace(entityProperty) 
                ? null 
                : ConvertCase(entityProperty);
        }

        public string Code(string code)
        {
            return this.autoResponseCodeFormatter.Format(code);
        }

        private string ConvertCase(string value)
        {
            return this.useCamelCase 
                ? value?.Camelize()
                : value?.Kebaberize();
        }
    }
}