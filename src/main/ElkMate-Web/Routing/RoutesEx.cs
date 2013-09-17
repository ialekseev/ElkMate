using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace SmartElk.ElkMate.Web.Routing
{
    public static class RoutesEx
    {
        public static void MapLowercaseRoute(this RouteCollection routes, string name, string url, object defaults)
        {
            routes.MapLowercaseRoute(name, url, defaults, null);
        }

        public static void MapLowercaseRoute(this RouteCollection routes, string name, string url, object defaults,
                                         object constraints)
        {
            if (routes == null)
                throw new ArgumentNullException("routes");

            if (url == null)
                throw new ArgumentNullException("url");

            var route = new LowercaseRoute(url, new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(defaults),
                Constraints = new RouteValueDictionary(constraints)
            };

            if (String.IsNullOrEmpty(name))
                routes.Add(route);
            else
                routes.Add(name, route);
        }        
    }
}