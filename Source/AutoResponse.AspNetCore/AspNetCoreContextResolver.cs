using AutoResponse.Core.Mappers;

namespace AutoResponse.AspNetCore
{
    public class AspNetCoreContextResolver : IContextResolver
    {
        public bool IncludeFullDetails(object context)
        {
            return false;
        }
    }
}