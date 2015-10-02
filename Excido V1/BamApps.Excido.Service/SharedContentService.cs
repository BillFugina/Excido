using System;
using System.Linq;
using BamApps.Excido.Data.Model;
using BamApps.Excido.Interface.Data;

namespace BamApps.Excido.Service {

    public class SharedContentService {
        readonly IDataContext _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SharedContent"/> class.
        /// </summary>
        /// <param name="dataContext"></param>
        public SharedContentService(IDataContext dataContext) {
            _dataContext = dataContext;
        }

        public Guid AddSharedContent(SharedContentUnit sharedContent) {
            Guid result = Guid.Empty;
            using (var transaction = _dataContext.BeginTransaction()) {
                try {
                    result = _dataContext.AddEntity(sharedContent);
                    _dataContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception) {
                    transaction.Rollback();
                    throw;
                }
            }
            return result;
        }

        public IQueryable<SharedContentUnit> GetSharedContent() {
            return _dataContext.QueryAll<SharedContentUnit>();
        }

    }
}
