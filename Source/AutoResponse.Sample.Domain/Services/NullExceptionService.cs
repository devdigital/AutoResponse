// <copyright file="NullExceptionService.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Sample.Domain.Services
{
    /// <summary>
    /// Null exception service.
    /// </summary>
    /// <seealso cref="AutoResponse.Sample.Domain.Services.IExceptionService" />
    public class NullExceptionService : IExceptionService
    {
        /// <inheritdoc />
        public void Execute()
        {
        }
    }
}