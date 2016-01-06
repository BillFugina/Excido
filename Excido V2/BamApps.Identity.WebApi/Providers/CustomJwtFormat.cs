using BamApps.Identity.WebApi.Entities;
using BamApps.Identity.WebApi.Repository;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Web;
using Thinktecture.IdentityModel.Tokens;
using System.Diagnostics;

namespace BamApps.Identity.WebApi.Providers {
    public class CustomJwtFormat : ISecureDataFormat<AuthenticationTicket> {

        private readonly string _issuer = string.Empty;

        public CustomJwtFormat(string issuer) {
            _issuer = issuer;
        }

        public string Protect(AuthenticationTicket data) {
            if (data == null) {
                throw new ArgumentNullException(nameof(data));
            }

            Client client;
            string clientId = data.Properties.Dictionary["as:client_id"];

            using (AuthRepository _repo = new AuthRepository()) {
                client = _repo.FindClient(clientId);
                Debug.Assert(client != null, "client is null.");
            }


            string audienceId = client.Audience.Id;
            string audienceSecret = client.Audience.Secret;

            var keyByteArray = TextEncodings.Base64Url.Decode(audienceSecret);

            var signingKey = new HmacSigningCredentials(keyByteArray);

            var issued = data.Properties.IssuedUtc;

            var expires = data.Properties.ExpiresUtc;

            var token = new JwtSecurityToken(_issuer, audienceId, data.Identity.Claims, issued.Value.UtcDateTime, expires.Value.UtcDateTime, signingKey);

            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.WriteToken(token);

            return jwt;
        }

        public AuthenticationTicket Unprotect(string protectedText) {
            throw new NotImplementedException();
        }

    }
}