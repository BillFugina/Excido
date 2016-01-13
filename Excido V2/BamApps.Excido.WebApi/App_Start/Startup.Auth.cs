using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using BamApps.Excido.WebApi.Providers;
using BamApps.Excido.WebApi.Models;
using System.Configuration;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;

namespace BamApps.Excido.WebApi
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
        }

        private void ConfigureOAuthTokenConsumption(IAppBuilder app) {

            var issuer = ConfigurationManager.AppSettings["as:Issuer"]; ;
            string audienceId = ConfigurationManager.AppSettings["as:AudienceId"];
            var audienceSecret = ConfigurationManager.AppSettings["as:AudienceSecret"];

            byte[] keyByteArray = TextEncodings.Base64Url.Decode(audienceSecret);

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { audienceId },
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[] {
                        new SymmetricKeyIssuerSecurityTokenProvider(issuer, keyByteArray)
                    }
                });

            var OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);
        }
    }
}
