using System.Collections.Generic;
using System.Linq;
using AutoFixture.Xunit2;
using AutoResponse.Core.Exceptions;
using AutoResponse.Core.Models;
using Xunit;

namespace AutoResponse.WebApi2.IntegrationTests.Tests.ExceptionTests
{
    public class ExceptionTests
    {
        [Theory]
        [AutoData]
        public void EntityNotFoundQueryExceptionMessageContainsParameters(
            string entityType, 
            string entityId)
        {
            var parameters = new List<QueryParameter>
            {
                new QueryParameter("foo", "bar"),
                new QueryParameter("baz", "foo")
            };

            var exception = new EntityNotFoundQueryException(
                entityType, 
                entityId, 
                parameters);

            Assert.True(exception.Message.Contains(parameters.First().Key));
        }
    }
}
