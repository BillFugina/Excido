using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Cors;

namespace BamApps.Excido.WebApi {
    public static class WebApiConfig {
        public static void Register(HttpConfiguration config) {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            var cors = new EnableCorsAttribute("http://www.excido.net, http://www.excido.info, http://excido.net, http://excido.info, http://excido.azurewebsites.net, http://localhost:59424", "*", "*");
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(name: "DefaultApi", 
                routeTemplate: "api/{controller}/{action}", 
                defaults: new { id = RouteParameter.Optional
                }
            );

            config.Routes.MapHttpRoute(
                name: "SlugRouterApi",
                routeTemplate: "{slug}",
                defaults: new {
                    controller = "SlugRouter",
                    action = "GetSlug"
                }
            );


        }
    }
}
