using BamApps.Excido.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BamApps.Excido.WebApi.Controllers {
    public class SlugRouterController : ApiController {

        private readonly ISharedContentService _sharedContentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SlugRouterController"/> class.
        /// </summary>
        public SlugRouterController(ISharedContentService sharedContentService) {
            _sharedContentService = sharedContentService;
        }

        [HttpGet]
        public HttpResponseMessage GetSlug(string slug) {
            HttpResponseMessage response;
            try {
                var content = _sharedContentService.GetSlugContent(slug);

                if (!string.IsNullOrEmpty(content)) {
                    response = Request.CreateResponse(HttpStatusCode.Redirect);
                    response.Headers.Location = new Uri(content);
                }
                else {
                    response = Request.CreateResponse(HttpStatusCode.NoContent);
                }
            }
            catch (Exception ex) {
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

            return response;
        }
    }
}
