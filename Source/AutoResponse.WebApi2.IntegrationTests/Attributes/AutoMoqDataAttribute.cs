// <copyright file="AutoMoqDataAttribute.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.WebApi2.IntegrationTests.Attributes
{
    using AutoFixture;
    using AutoFixture.AutoMoq;
    using AutoFixture.Xunit2;

    /// <summary>
    /// AutoMoq data attribute.
    /// </summary>
    /// <seealso cref="AutoFixture.Xunit2.AutoDataAttribute" />
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMoqDataAttribute"/> class.
        /// </summary>
        public AutoMoqDataAttribute()
            : base(() => new Fixture()
                .Customize(new AutoMoqCustomization()))
        {
        }
    }
}