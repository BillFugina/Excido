using BamApps.Identity.WebApi.Entities;
using BamApps.Identity.WebApi.Models;
using BamApps.Identity.WebApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BamApps.Identity.WebApi.Controllers {
    [RoutePrefix("api/audience")]
    public class AudienceController : ApiController {
        [Route("")]
        public IHttpActionResult Post(AudienceModel audienceModel) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            Audience newAudience = AudienceRepository.AddAudience(audienceModel.Name);

            return Ok<Audience>(newAudience);

        }
    }
}