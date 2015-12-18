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

#if DEV
            var cors = new EnableCorsAttribute("http://www.dev.excido.net, https://www.dev.excido.net, http://dev.excido.net, https://dev.excido.net, http://excido-dev.azurewebsites.net, http://localhost:59424, https://localhost:44301", "*", "*");
#else
            var cors = new EnableCorsAttribute("http://www.excido.net, https://www.excido.net, http://www.excido.info, http://excido.net, https://excido.net, http://excido.info, http://excido.azurewebsites.net, http://localhost:59424, https://localhost:44301", "*", "*");
#endif

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
