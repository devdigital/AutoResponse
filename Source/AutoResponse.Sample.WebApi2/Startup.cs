using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AutoResponse.Sample.WebApi2.Startup))]

namespace AutoResponse.Sample.WebApi2
{
    using System.Linq;
    using System.Net.Http.Formatting;
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using Autofac;
    using Autofac.Features.ResolveAnything;
    using Autofac.Integration.WebApi;

    using Newtonsoft.Json.Serialization;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {            
            var configuration = new HttpConfiguration();
            ConfigureRouting(configuration);
            ConfigureSerialization(configuration);
            var container = ConfigureContainer(configuration);

            var cors = new EnableCorsAttribute("*", "*", "*");
            configuration.EnableCors(cors);

            // Register the Autofac middleware FIRST, then the Autofac Web API middleware,
            // and finally the standard Web API middleware.
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(configuration);
            app.UseWebApi(configuration);
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

        private static IContainer ConfigureContainer(HttpConfiguration configuration)
        {
            var builder = new ContainerBuilder();

            // builder.RegisterType<StubFooRepository>().As<IFooRepository>().SingleInstance();
            // builder.RegisterGeneric(typeof(AutoMapperMapper<,>)).As(typeof(IMapper<,>)).SingleInstance();

            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            return container;
        }
    }
}
