namespace AutoResponse.Core.Formatters
{
    public class NullApiEventCodeFormatter : IApiEventCodeFormatter
    {
        public string Format(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return null;
            }

            return code;
        }
    }
}