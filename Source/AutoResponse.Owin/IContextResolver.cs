namespace AutoResponse.Owin
{
    public interface IContextResolver
    {
        bool IncludeFullDetails(object context);
    }
}