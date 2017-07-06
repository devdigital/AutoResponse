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

    using AutoResponse.Sample.Data.Repositories;
    using AutoResponse.Sample.Domain.Repositories;
    using AutoResponse.WebApi2.ExceptionHandling;

    using global::Owin;

    using Newtonsoft.Json.Serialization;

    using Owin;

    public class Bootstrapper
    {
        private readonly IAppBuilder app;

        private readonly Registrations registrations;

        public Bootstrapper(IAppBuilder app, Registrations registrations = null)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            this.app = app;
            this.registrations = registrations;
        }

        public void Run()
        {
            var configuration = new HttpConfiguration();

            ConfigureRouting(configuration);
            ConfigureSerialization(configuration);
            var container = this.ConfigureContainer(configuration);

            // configuration.Services.Replace(typeof(IExceptionHandler), new AutoResponseExceptionHandler());

            var cors = new EnableCorsAttribute("*", "*", "*");
            configuration.EnableCors(cors);

            this.app.Use<AutoResponseExceptionMiddleware>(new AutoResponseExceptionHttpResponseMapper());

            app.Use(
                async (context, next) =>
                    {
                        if (context.Request.Uri.AbsolutePath.StartsWith("/fail"))
                        {
                            throw new Exception("This is a test exception");
                        }

                        await next();
                    });

            // Register the Autofac middleware FIRST, then the Autofac Web API middleware,
            // and finally the standard Web API middleware.
            this.app.UseAutofacMiddleware(container);
            this.app.UseAutofacWebApi(configuration);
            this.app.UseWebApi(configuration);
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

            builder.RegisterType<DefaultValuesRepository>().As<IValuesRepository>();

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