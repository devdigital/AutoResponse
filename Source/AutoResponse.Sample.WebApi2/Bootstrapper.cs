// <copyright file="Bootstrapper.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace AutoResponse.Sample.WebApi2
{
    using System;
    using System.Linq;
    using System.Net.Http.Formatting;
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using System.Web.Http.ExceptionHandling;
    using Autofac;
    using Autofac.Features.ResolveAnything;
    using Autofac.Integration.WebApi;
    using AutoResponse.Core.Formatters;
    using AutoResponse.Core.Logging;
    using AutoResponse.Core.Mappers;
    using AutoResponse.Owin;
    using AutoResponse.Sample.Data.Repositories;
    using AutoResponse.Sample.Domain.Repositories;
    using AutoResponse.Sample.Domain.Services;
    using AutoResponse.Sample.WebApi2.Factories;
    using AutoResponse.WebApi2.ExceptionHandling;
    using global::Owin;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// Bootstrapper.
    /// </summary>
    public class Bootstrapper
    {
        private readonly IAppBuilder app;

        private readonly Registrations registrations;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bootstrapper"/> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="registrations">The registrations.</param>
        public Bootstrapper(IAppBuilder app, Registrations registrations = null)
        {
            this.app = app ?? throw new ArgumentNullException(nameof(app));
            this.registrations = registrations;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            var configuration = new HttpConfiguration();

            ConfigureRouting(configuration);
            ConfigureSerialization(configuration);
            var container = this.ConfigureContainer(configuration);

            configuration.Services.Replace(typeof(IExceptionHandler), new AutoResponseExceptionHandler());

            var includeFullDetails = container.Resolve<ISettingsService>().GetIncludeFullDetails();
            configuration.IncludeErrorDetailPolicy =
                includeFullDetails ? IncludeErrorDetailPolicy.Always : IncludeErrorDetailPolicy.Never;

            var cors = new EnableCorsAttribute("*", "*", "*");
            configuration.EnableCors(cors);

            this.app.UseAutoResponse(new AutoResponseOptions
            {
                Logger = GetService<IAutoResponseLogger>(configuration),
                EventHttpResponseMapper = new SampleHttpResponseMapper(
                    new OwinContextResolver(),
                    GetService<IAutoResponseExceptionFormatter>(configuration)),
            });

            // Allow easier testing of responses via browser
            this.app.Use(
               async (context, next) =>
               {
                   if (context.Request.Uri.AbsolutePath.StartsWith("/fail"))
                   {
                       throw new Exception("There was an error");
                   }

                   await next();
               });

            // Allow automated tests of OWIN exceptions
            this.app.Use(
                async (context, next) =>
                    {
                        var exceptionService = GetService<IExceptionService>(configuration);
                        exceptionService.Execute();
                        await next();
                    });

            this.app.UseAutofacMiddleware(container);
            this.app.UseAutofacWebApi(configuration);
            this.app.UseWebApi(configuration);
        }

        private static TService GetService<TService>(HttpConfiguration configuration)
            where TService : class
        {
            if (!(configuration?.DependencyResolver
                ?.GetService(typeof(TService)) is TService service))
            {
                throw new InvalidOperationException($"No {typeof(TService).Name} registered in container");
            }

            return service;
        }

        private static void ConfigureRouting(HttpConfiguration configuration)
        {
            configuration.MapHttpAttributeRoutes();
        }

        private static void ConfigureSerialization(HttpConfiguration configuration)
        {
            var jsonFormatter = configuration.Formatters.OfType<JsonMediaTypeFormatter>().FirstOrDefault();
            if (jsonFormatter == null)
            {
                return;
            }

            jsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
        }

        private IContainer ConfigureContainer(HttpConfiguration configuration)
        {
            var builder = new ContainerBuilder();

            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<DefaultSettingsService>().As<ISettingsService>();
            builder.RegisterType<NullExceptionService>().As<IExceptionService>();
            builder.RegisterType<DefaultValuesRepository>().As<IValuesRepository>();
            builder.RegisterType<OkActionResultFactory>().As<IHttpActionResultFactory>();

            builder.RegisterType<NullAutoResponseLogger>().As<IAutoResponseLogger>();
            builder.RegisterType<AutoResponseExceptionFormatter>().As<IAutoResponseExceptionFormatter>();
            builder.RegisterType<SampleHttpResponseMapper>().As<IApiEventHttpResponseMapper>();
            builder.RegisterType<WebApiContextResolver>().As<IContextResolver>();

            this.AdditionalRegistrations(builder);

            var container = builder.Build();
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            return container;
        }

        private void AdditionalRegistrations(ContainerBuilder builder)
        {
            if (this.registrations != null)
            {
                // Register additional registrations provided to the bootstrapper
                // These will override any previous registrations
                foreach (var typeRegistration in this.registrations.TypeRegistrations)
                {
                    builder.RegisterType(typeRegistration.Value).As(typeRegistration.Key);
                }

                foreach (var instanceRegistration in this.registrations.InstanceRegistrations)
                {
                    builder.RegisterInstance(instanceRegistration.Value).As(instanceRegistration.Key);
                }
            }
        }
    }
}