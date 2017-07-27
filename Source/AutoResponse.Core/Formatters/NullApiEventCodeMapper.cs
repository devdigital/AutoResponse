namespace AutoResponse.Core.Formatters
{
    public class NullApiEventCodeMapper : IApiEventCodeMapper
    {
        public string GetCode(object apiEvent)
        {
            return null;
        }
    }
}