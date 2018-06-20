// <copyright file="AutoInjectBuilderCustomization.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.WebApi2.IntegrationTests.Helpers
{
    using AutoFixture;

    /// <summary>
    /// Auto inject builder customization.
    /// </summary>
    /// <seealso cref="AutoFixture.ICustomization" />
    internal class AutoInjectBuilderCustomization : ICustomization
    {
        /// <inheritdoc />
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new AutoInjectBuilder());
        }
    }
}