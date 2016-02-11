using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNet.Identity;

namespace BamApps.Identity.WebApi.Models {

    public class RegisterExternalBindingModel : IExternalLoginInfo {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Provider { get; set; }

        [Required]
        public string ExternalAccessToken { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string ClientId { get; set; }
    }

    public abstract class ExternalLoginBase : IExternalLoginInfo {
        public string Provider { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ExternalAccessToken { get; set; }

        protected Claim _providerKeyClaim;
        protected ExternalLoginBase() {
        }

        public ExternalLoginBase(ClaimsIdentity identity) {
            if (identity == null) {
                throw new ArgumentNullException(nameof(identity), $"{nameof(identity)} is null.");
            }

            _providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
            if (_providerKeyClaim == null || String.IsNullOrEmpty(_providerKeyClaim.Issuer) || String.IsNullOrEmpty(_providerKeyClaim.Value)) {
                throw new ArgumentException($"{nameof(identity)} must include a {nameof(ClaimTypes.NameIdentifier)} ClaimType.");
            }

            if (_providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer) {
                throw new ArgumentException($"{nameof(identity)} {nameof(ClaimTypes.NameIdentifier)} Claim {nameof(_providerKeyClaim.Issuer)} cannot be {ClaimsIdentity.DefaultIssuer}");
            }

            Provider = _providerKeyClaim.Issuer;
            UserName = identity.FindFirstValue(ClaimTypes.Name);
            FirstName = identity.FindFirstValue(ClaimTypes.GivenName);
            LastName = identity.FindFirstValue(ClaimTypes.Surname);
            Email = identity.FindFirstValue(ClaimTypes.Email);
            ExternalAccessToken = identity.FindFirstValue("ExternalAccessToken");
        }
    }

    public class ExternalLoginData : ExternalLoginBase {
        public string ProviderKey { get; set; }

        protected ExternalLoginData(ClaimsIdentity identity) : base(identity) {
            ProviderKey = _providerKeyClaim.Value;
        }

        public static ExternalLoginData FromIdentity(ClaimsIdentity claimsIdentity) {
            return new ExternalLoginData(claimsIdentity);
        }

        public ExternalLoginResponse GenerateResponse(bool isRegistered) {
            return new ExternalLoginResponse(this, isRegistered);
        }
    }

    public class ExternalLoginResponse : ExternalLoginBase {
        public bool IsRegistered { get; set; }

        protected internal ExternalLoginResponse(ExternalLoginBase externalLogin, bool isRegistered) {
            Provider = externalLogin.Provider;
            UserName = externalLogin.UserName;
            FirstName = externalLogin.FirstName;
            LastName = externalLogin.LastName;
            Email = externalLogin.Email;
            ExternalAccessToken = externalLogin.ExternalAccessToken;
            IsRegistered = isRegistered;
        }
    }


    public class ExternalLoginViewModel {
        public string Name { get; set; }

        public string Url { get; set; }

        public string State { get; set; }
    }

    public class ParsedExternalAccessToken {
        public string user_id { get; set; }
        public string app_id { get; set; }
        public string email { get; set; }
    }

    public class GoogleUserInfo {
        public string id { get; set; }
        public string email { get; set; }
        public bool verified_email { get; set; }
        public string name { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
        public string link { get; set; }
        public string picture { get; set; }
        public string gender { get; set; }
        public string locale { get; set; }

    }
}