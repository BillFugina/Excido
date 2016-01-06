using BamApps.Identity.WebApi.Infrastructure;
using BamApps.Identity.WebApi.Providers;
using BamApps.Identity.WebApi.Repository;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using BamApps.Identity.WebApi.Entities;

namespace BamApps.Identity.WebApi {
    public class Startup {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        public static OAuthAuthorizationServerOptions OAuthServerOptions { get; private set; }

        public static GoogleOAuth2AuthenticationOptions googleAuthOptions { get; private set; }
        public static FacebookAuthenticationOptions facebookAuthOptions { get; private set; }

        public void Configuration(IAppBuilder app) {

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseForcedHttps(44300);

            ConfigureUserAndRoleManagers(app);

            ConfigureOAuthTokenGeneration(app);
            ConfigureSocialOAuth(app);
            ConfigureOAuthTokenConsumption(app);

            HttpConfiguration httpConfig = new HttpConfiguration();
            ConfigureWebApi(httpConfig);
            app.UseWebApi(httpConfig);
        }
        private void ConfigureUserAndRoleManagers(IAppBuilder app) {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
        }

        public void ConfigureOAuthTokenGeneration(IAppBuilder app) {
            OAuthServerOptions = new OAuthAuthorizationServerOptions() {
                AllowInsecureHttp = false,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new SimpleAuthorizationServerProvider(),
                AccessTokenFormat = new CustomJwtFormat("https://localhost:44300/"),
                RefreshTokenProvider = new SimpleRefreshTokenProvider()
            };

            app.UseOAuthAuthorizationServer(OAuthServerOptions);
        }


        private void ConfigureSocialOAuth(IAppBuilder app) {
            //use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);

            //Configure Google External Login
            googleAuthOptions = new GoogleOAuth2AuthenticationOptions() {
                ClientId = "775348847368-d6h37j3bu9bun5m4msl85229mqc6mvjf.apps.googleusercontent.com",
                ClientSecret = "Mca7VhaN-26iPcgM2WDZZ26M",
                Provider = new GoogleAuthProvider()
            };

            app.UseGoogleAuthentication(googleAuthOptions);
        }


        private void ConfigureOAuthTokenConsumption(IAppBuilder app) {

            var issuer = "https://localhost:44300/";
            string audienceId = ConfigurationManager.AppSettings["as:AudienceId"];
            Audience audience = null;

            using (AuthRepository _repo = new AuthRepository()) {
                audience = _repo.FindAudience(audienceId);
            }

            if (audienceId == null) {
                throw new Exception("Could not find Audience");
            }

            var audienceSecret = audience.Secret;
            byte[] keyByteArray = TextEncodings.Base64Url.Decode(audienceSecret);

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions {
                    TokenHandler = new CustomJwtTokenHandler(),
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { audienceId },
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[] {
                        new SymmetricKeyIssuerSecurityTokenProvider(issuer, keyByteArray)
                    }
                });

            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);
        }


        private void ConfigureWebApi(HttpConfiguration config) {
            config.MapHttpAttributeRoutes();

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}