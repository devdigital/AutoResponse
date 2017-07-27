namespace AutoResponse.Core.Formatters
{
    public interface IApiEventCodeMapper
    {
        string GetCode(object exception);
    }
}