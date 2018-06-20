// <copyright file="AutoCustomData.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.WebApi2.IntegrationTests.Helpers
{
    using AutoFixture;
    using AutoFixture.Xunit2;

    /// <summary>
    /// Auto custom data.
    /// </summary>
    /// <seealso cref="AutoFixture.Xunit2.AutoDataAttribute" />
    public class AutoCustomData : AutoDataAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCustomData"/> class.
        /// </summary>
        public AutoCustomData()
            : base(() => new Fixture().Customize(new AutoInjectBuilderCustomization()))
        {
        }
    }
}