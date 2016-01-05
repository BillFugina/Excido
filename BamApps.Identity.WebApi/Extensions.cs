using Microsoft.Owin;
using Microsoft.Owin.Security.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BamApps.Identity.WebApi {
    public static class Extensions {
        public static void AddOrChangeHeader(this IHeaderDictionary headerDictionary, string header, string value) {
            if (headerDictionary.ContainsKey(header)) {
                headerDictionary.Set(header, value);
            }
            else {
                headerDictionary.Add(header, new[] { value });
            }
        }
    }
}