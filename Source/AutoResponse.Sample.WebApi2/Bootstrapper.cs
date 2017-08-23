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
    using AutoResponse.Sample.Data.Repositories;
    using AutoResponse.Sample.Domain.Repositories;
    using AutoResponse.Sample.Domain.Services;
    using AutoResponse.Sample.WebApi2.Factories;
    using AutoResponse.WebApi2.ExceptionHandling;
    using AutoResponse.WebApi2.Logging;

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

            configuration.Services.Replace(typeof(IExceptionHandler), new AutoResponseExceptionHandler());

            var includeFullDetails = container.Resolve<ISettingsService>().GetIncludeFullDetails();
            configuration.IncludeErrorDetailPolicy = 
                includeFullDetails ? IncludeErrorDetailPolicy.Always : IncludeErrorDetailPolicy.Never;

            configuration.Services.Add(typeof(IExceptionLogger), new DefaultExceptionLogger(null));

            var cors = new EnableCorsAttribute("*", "*", "*");
            configuration.EnableCors(cors);

            this.app.UseAutoResponse(new AutoResponseOptions
            {
                Logger = GetService<IAutoResponseLogger>(configuration),
                EventHttpResponseMapper = new SampleHttpResponseMapper(
                    new OwinContextResolver(), 
                    GetService<IAutoResponseExceptionFormatter>(configuration))
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

        private static TService GetService<TService>(HttpConfiguration configuration) where TService : class
        {
            var service = configuration?.DependencyResolver
                ?.GetService(typeof(TService)) as TService;

            if (service == null)
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