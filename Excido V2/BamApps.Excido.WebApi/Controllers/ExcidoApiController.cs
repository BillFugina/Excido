using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BamApps.Excido.WebApi.Controllers {
    [RoutePrefix("api/Excido")]
    public class ExcidoApiController : ApiController {

        [HttpGet]
        [Authorize]
        [Route("verify")]
        public bool Verify() {
            return true;
        }
    }
}
