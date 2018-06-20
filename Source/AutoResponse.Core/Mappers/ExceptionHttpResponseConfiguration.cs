// <copyright file="ExceptionHttpResponseConfiguration.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Core.Mappers
{
    using System;
    using System.Collections.Generic;

    using AutoResponse.Core.Responses;

    /// <summary>
    /// Exception HTTP response configuration.
    /// </summary>
    public class ExceptionHttpResponseConfiguration
    {
        private readonly IDictionary<Type, Func<ExceptionHttpResponseContext, object, IHttpResponse>> mappings;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHttpResponseConfiguration"/> class.
        /// </summary>
        /// <param name="mappings">The mappings.</param>
        public ExceptionHttpResponseConfiguration(IDictionary<Type, Func<ExceptionHttpResponseContext, object, IHttpResponse>> mappings)
        {
            this.mappings = mappings ?? throw new ArgumentNullException(nameof(mappings));
        }

        /// <summary>
        /// Adds a mapping.
        /// </summary>
        /// <typeparam name="TApiEvent">The type of the API event.</typeparam>
        /// <param name="mapping">The mapping.</param>
        public void AddMapping<TApiEvent>(
            Func<ExceptionHttpResponseContext, TApiEvent, IHttpResponse> mapping)
            where TApiEvent : class
        {
            if (mapping == null)
            {
                throw new ArgumentNullException(nameof(mapping));
            }

            if (this.mappings.ContainsKey(typeof(TApiEvent)))
            {
                throw new InvalidOperationException(
                    $"API event type {typeof(TApiEvent).Name} mapping already registered");
            }

            this.mappings.Add(
                typeof(TApiEvent),
                (c, e) => mapping(c, e as TApiEvent));
        }

        /// <summary>
        /// Updates a mapping.
        /// </summary>
        /// <typeparam name="TApiEvent">The type of the API event.</typeparam>
        /// <param name="mapping">The mapping.</param>
        public void UpdateMapping<TApiEvent>(Func<ExceptionHttpResponseContext, TApiEvent, IHttpResponse> mapping)
            where TApiEvent : class
        {
            if (mapping == null)
            {
                throw new ArgumentNullException(nameof(mapping));
            }

            if (!this.mappings.ContainsKey(typeof(TApiEvent)))
            {
                throw new InvalidOperationException(
                    $"API event type {typeof(TApiEvent).Name} mapping is not registered");
            }

            this.mappings[typeof(TApiEvent)] = (c, e) => mapping(c, e as TApiEvent);
        }

        /// <summary>
        /// Removes a mapping.
        /// </summary>
        /// <typeparam name="TApiEvent">The type of the API event.</typeparam>
        public void RemoveMapping<TApiEvent>()
        {
            if (!this.mappings.ContainsKey(typeof(TApiEvent)))
            {
                throw new InvalidOperationException(
                    $"API event type {typeof(TApiEvent).Name} mapping is not registered");
            }

            this.mappings.Remove(typeof(TApiEvent));
        }
    }
}