namespace AutoResponse.Core.Exceptions
{
    using System.Collections.Generic;

    using AutoResponse.Core.Models;

    public interface IEntityNotFoundQueryException
    {
        string EntityType { get; }

        IEnumerable<QueryParameter> Parameters { get; }
    }
}