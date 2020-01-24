using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OrderingSystem.WebApp
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapMvcAttributeRoutes();

			routes.MapRoute(
                name:"EmptyRoute",
                url : "",
                defaults: new { controller = "Home", action = "Index", page = 0 }
                );

            routes.MapRoute(
                name:"HomePaging",
                url : "Home/Index/{page}",
                defaults: new { controller = "Home", action = "Index", page = 0 }
                );

            routes.MapRoute(
				name: "Default",
                url: "{controller}/{action}/{page}",
                defaults: new { controller = "Home", action = "Index" , page = 0 }
				//url: "{controller}/{action}/{id}",
				//defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
