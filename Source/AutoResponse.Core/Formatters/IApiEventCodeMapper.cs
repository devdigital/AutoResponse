namespace AutoResponse.Core.Formatters
{
    using AutoResponse.Core.ApiEvents;

    public interface IApiEventCodeMapper
    {
        string GetCode(AutoResponseApiEvent exception);
    }
}