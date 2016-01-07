using Microsoft.Owin.Security.Facebook;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BamApps.Identity.WebApi.Providers {
    public class FacebookAuthProvider : FacebookAuthenticationProvider {
        public override Task Authenticated(FacebookAuthenticatedContext context) {
            context.Identity.AddClaim(new Claim("ExternalAccessToken", context.AccessToken));

            foreach (var claim in context.User) {
                string claimType;

                switch (claim.Key) {
                    case "first_name":
                        claimType = ClaimTypes.GivenName;
                        break;
                    case "last_name":
                        claimType = ClaimTypes.Surname;
                        break;
                    default:
                        claimType = string.Format("urn:facebook:{0}", claim.Key);
                        break;
                }

                string claimValue = claim.Value.ToString();
                if (!context.Identity.HasClaim(claimType, claimValue))
                    context.Identity.AddClaim(new System.Security.Claims.Claim(claimType, claimValue, "XmlSchemaString", "Facebook"));

            }

            return Task.FromResult<object>(null);
        }
    }
}