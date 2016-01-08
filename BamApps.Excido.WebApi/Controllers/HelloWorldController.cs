using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BamApps.Excido.WebApi.Controllers {
    [RoutePrefix("api/HelloWorld")]
    public class HelloWorldController : ApiController {

        [HttpGet]
        [Route("anonymous")]
        public string AnonymousHellowWorld() {
            return "Anonymous  Hello World";
        }

        [HttpGet]
        [Route("authorize")]
        [Authorize]
        public string AuthorizedHellowWorld() {
            return "Authorized Hello World";
        }
    }
}