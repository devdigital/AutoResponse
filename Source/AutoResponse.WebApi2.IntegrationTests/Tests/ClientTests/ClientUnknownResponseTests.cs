// <copyright file="ClientUnknownResponseTests.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.WebApi2.IntegrationTests.Tests.ClientTests
{
    using System;
    using System.Threading.Tasks;
    using AutoFixture.Xunit2;
    using AutoResponse.Client;
    using AutoResponse.WebApi2.IntegrationTests.Helpers;
    using Xunit;

    // ReSharper disable StyleCop.SA1600
    #pragma warning disable SA1600
    #pragma warning disable 1591

    public class ClientUnknownResponseTests
    {
        [Theory]
        [AutoData]
        public async Task UnknownResponseExceptionContainsRequest(
            SampleServerFactory serverFactory)
        {
            using (var server = serverFactory.Create())
            {
                var response = await server.HttpClient.GetAsync("/foo");
                var exception = await Assert.ThrowsAsync<Exception>(() => response.HandleErrors());
                Assert.True(exception.Message.Contains("Request"));
            }
        }

        [Theory]
        [AutoData]
        public async Task UnknownResponseExceptionContainsResponse(
            SampleServerFactory serverFactory)
        {
            using (var server = serverFactory.Create())
            {
                var response = await server.HttpClient.GetAsync("/foo");
                var exception = await Assert.ThrowsAsync<Exception>(() => response.HandleErrors());
                Assert.True(exception.Message.Contains("Response"));
            }
        }
    }
}