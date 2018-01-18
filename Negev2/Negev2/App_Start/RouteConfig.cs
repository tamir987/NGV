using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Negev2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Home",
                url: "Home",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Analayzer",
                url: "ניתוח_תכנון_קיים",
                defaults: new { controller = "Analayzer", action = "AnalayzerIndex", id = UrlParameter.Optional }
            );
            routes.MapRoute(
              name: "Areas",
              url: "איזורי_התכנות",
              defaults: new { controller = "Areas", action = "AreasIndex", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "History",
              url: "היסטוריית_חלקות",
              defaults: new { controller = "History", action = "HistoryIndex", id = UrlParameter.Optional }
            );


            routes.MapRoute(
              name: "Optimal",
              url: "הכנסת_גידולים",
              defaults: new { controller = "Optimal", action = "OptimalIndex", id = UrlParameter.Optional }
            );

            routes.MapRoute(
             name: "Database",
             url: "Database",
             defaults: new { controller = "Database", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
