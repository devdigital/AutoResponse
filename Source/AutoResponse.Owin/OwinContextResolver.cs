namespace AutoResponse.Owin
{
    public class OwinContextResolver : IContextResolver
    {
        public bool IncludeFullDetails(object context)
        {
            return false;
        }
    }
}