using Autofac;
using Autofac.Integration.WebApi;
using Clientes.WebApi.Interfaces;
using Clientes.WebApi.Models;
using Clientes.WebApi.Repositories;
using Clientes.WebApi.Repositories.EF;
using Clientes.WebApi.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Web.Http;

namespace Clientes.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Autofac dependency injection
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<ClientesDbContext>().InstancePerRequest();

            builder.RegisterType<Repository<Cliente>>()
                .As<IRepository<Cliente>>()
                .InstancePerRequest();

            builder.RegisterType<ClienteService>()
                .As<IClienteService>()
                .InstancePerRequest();

            var container = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

            // camelCase JSON
            var jsonFormatter = config.Formatters.JsonFormatter;

            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonFormatter.SerializerSettings.Formatting = Formatting.Indented;

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
