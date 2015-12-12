using System;
using System.Linq;
using BamApps.Excido.Data.Model;
using BamApps.Excido.Interface.Data;

namespace BamApps.Excido.Service {

    public class SharedContentRepository : BaseService<SharedContentUnit> {
        public SharedContentRepository(IDataContext dataContext) : base(dataContext, NullServiceValidator<SharedContentUnit>.Instance()) {
        }
    }

}
