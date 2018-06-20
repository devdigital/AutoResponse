// <copyright file="Registrations.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Sample.WebApi2
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Registrations.
    /// </summary>
    public class Registrations
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Registrations"/> class.
        /// </summary>
        /// <param name="typeRegistrations">The type registrations.</param>
        /// <param name="instanceRegistrations">The instance registrations.</param>
        public Registrations(
            IDictionary<Type, Type> typeRegistrations,
            IDictionary<Type, object> instanceRegistrations)
        {
            this.TypeRegistrations = typeRegistrations;
            this.InstanceRegistrations = instanceRegistrations;
        }

        /// <summary>
        /// Gets the type registrations.
        /// </summary>
        /// <value>
        /// The type registrations.
        /// </value>
        public IDictionary<Type, Type> TypeRegistrations { get; }

        /// <summary>
        /// Gets the instance registrations.
        /// </summary>
        /// <value>
        /// The instance registrations.
        /// </value>
        public IDictionary<Type, object> InstanceRegistrations { get; }
    }
}