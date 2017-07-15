namespace AutoResponse.Core.Mappers
{
    public interface IContextResolver
    {
        bool IncludeFullDetails(object context);
    }
}