namespace AutoResponse.Owin
{
    using AutoResponse.Core.Mappers;

    public class OwinContextResolver : IContextResolver
    {
        public bool IncludeFullDetails(object context)
        {
            return false;
        }
    }
}