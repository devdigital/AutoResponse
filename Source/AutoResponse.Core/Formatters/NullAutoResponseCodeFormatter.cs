namespace AutoResponse.Core.Formatters
{
    public class NullAutoResponseCodeFormatter : IAutoResponseCodeFormatter
    {
        public string Format(string code)
        {
            return string.IsNullOrWhiteSpace(code) ? null : code;
        }
    }
}