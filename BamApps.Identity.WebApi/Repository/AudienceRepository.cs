using BamApps.Identity.WebApi.Entities;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace BamApps.Identity.WebApi.Repository {
    public class AudienceRepository {
        public static ConcurrentDictionary<string, Audience> AudiencesList = new ConcurrentDictionary<string, Audience>();

        static AudienceRepository() {
            AudiencesList.TryAdd("099153c2625149bc8ecb3e85e03f0022",
                                new Audience {
                                    Id = "099153c2625149bc8ecb3e85e03f0022",
                                    Secret = "IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw",
                                    Name = "ResourceServer.Api 1"
                                });
        }

        public static Audience AddAudience(string name) {
            var clientId = Guid.NewGuid().ToString("N");

            var key = new byte[32];
            RNGCryptoServiceProvider.Create().GetBytes(key);
            var base64Secret = TextEncodings.Base64Url.Encode(key);

            Audience newAudience = new Audience { Id = clientId, Secret = base64Secret, Name = name };
            AudiencesList.TryAdd(clientId, newAudience);
            return newAudience;
        }

        public static Audience FindAudience(string clientId) {
            Audience audience = null;
            if (AudiencesList.TryGetValue(clientId, out audience)) {
                return audience;
            }
            return null;
        }
    }
}