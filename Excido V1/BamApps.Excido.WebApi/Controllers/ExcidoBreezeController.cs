using BamApps.Excido.Data.Context;
using BamApps.Excido.Data.Model;
using BamApps.Excido.Service;
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
    [BreezeController]
    public class ExcidoBreezeController : ApiController {
        readonly IBreezeContextProvider _contextProvider;

        public ExcidoBreezeController(IBreezeContextProvider _contextProvider) {
            this._contextProvider = _contextProvider;
        }



        [HttpGet]
        public string Metadata() {
            return new EFContextProvider<ExcidoContext>().Metadata();
        }

        [HttpGet]
        public IQueryable<SharedContentUnit> SharedContentUnits() {
            return _contextProvider.SharedContentUnits();
        }

        [HttpPost]
        public SaveResult SaveChanges(JObject saveBundle) {
            return _contextProvider.SaveChanges(saveBundle);
        }
    }
}
