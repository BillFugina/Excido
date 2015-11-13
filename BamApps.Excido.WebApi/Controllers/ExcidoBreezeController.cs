using BamApps.Excido.Data.Context;
using BamApps.Excido.Data.Model;
using Breeze.ContextProvider;
using Breeze.ContextProvider.EF6;
using Breeze.WebApi2;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BamApps.Excido.WebApi.Controllers {
    [EnableCors(origins: "http://excido.azurewebsites.net, http://localhost:59424/", headers: "*", methods: "*")]
    [BreezeController]
    public class ExcidoBreezeController : ApiController {
        readonly EFContextProvider<ExcidoContext> _contextProvider = new EFContextProvider<ExcidoContext>();


        [HttpGet]
        public string Metadata() {
            return _contextProvider.Metadata();
        }

        [HttpGet]
        public IQueryable<SharedContentUnit> SharedContentUnits() {
            return _contextProvider.Context.SharedContentUnits;
        }

        [HttpPost]
        public SaveResult SaveChanges(JObject saveBundle) {
            return _contextProvider.SaveChanges(saveBundle);
        }
    }
}
