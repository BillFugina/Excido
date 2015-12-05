using System.Linq;
using BamApps.Excido.Data.Model;
using Breeze.ContextProvider;
using Newtonsoft.Json.Linq;

namespace BamApps.Excido.Service {
    public interface IBreezeContextProvider {
        IQueryable<SharedContentUnit> SharedContentUnits();

        SaveResult SaveChanges(JObject saveBundle, TransactionSettings transactionSettings = null);
    }
}