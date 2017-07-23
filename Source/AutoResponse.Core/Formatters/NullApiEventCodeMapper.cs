namespace AutoResponse.Core.Formatters
{
    using AutoResponse.Core.ApiEvents;

    public class NullApiEventCodeMapper : IApiEventCodeMapper
    {
        public string GetCode(AutoResponseApiEvent apiEvent)
        {
            return null;
        }
    }
}