using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Web;

namespace BamApps.Identity.WebApi.Providers {
    public class CustomJwtTokenHandler : JwtSecurityTokenHandler {
        protected override void ValidateAudience(IEnumerable<string> audiences, SecurityToken securityToken, TokenValidationParameters validationParameters) {
            base.ValidateAudience(audiences, securityToken, validationParameters);
        }

        
    }
}