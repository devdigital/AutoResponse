using System.Collections.Generic;

namespace AutoResponse.Core.Dtos
{
    public class ResourceNotFoundQueryApiModel : ErrorApiModel
    {
        public string Resource { get; set; }

        public IEnumerable<QueryParameterDto> QueryParameters { get; set; }
    }
}