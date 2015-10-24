using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Fridge.Models.Abstract;
using Fridge.Models.Concrete;
using Fridge.Models.Concrete.Entities;
using Fridge.Models.Concrete.Repositories;
using Fridge.Service;
using Microsoft.Practices.Unity;

namespace Fridge
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      // Web API configuration and services
      // Web API configuration and services
      var container = new UnityContainer();
      container.RegisterType<ICommonRepository<User>, UserRepository>(new HierarchicalLifetimeManager());
      container.RegisterType<ICommonRepository<Product>, ProductRepository>(new HierarchicalLifetimeManager());

      config.DependencyResolver = new UnityResolver(container);

      // Web API routes
      config.MapHttpAttributeRoutes();

      config.Routes.MapHttpRoute(
          name: "DefaultApi",
          routeTemplate: "api/{controller}/{id}",
          defaults: new { id = RouteParameter.Optional }
      );

      config.Formatters.Remove(config.Formatters.XmlFormatter);
    }
  }
}
