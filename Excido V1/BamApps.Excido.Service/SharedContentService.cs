using System;
using System.Linq;
using System.Linq.Expressions;
using BamApps.Excido.Data.Model;
using BamApps.Excido.Interface.Data;
using BamApps.Excido.Interface.Service;

namespace BamApps.Excido.Service {

    public class SharedContentService : BaseService<SharedContentUnit>, ISharedContentService {

        /// <summary>
        /// Initializes a new instance of the <see cref="SharedContent"/> class.
        /// </summary>
        /// <param name="dataContext"></param>
        public SharedContentService(IDataContext dataContext) : base(dataContext) {
        }

        public string GetSlugContent(string slug) {
            var result = "";
            var sharedContentUnit = _dataContext.QueryAllWhere<SharedContentUnit>(s => s.Slug == slug).SingleOrDefault();
            if (sharedContentUnit != null) {
                var expireDate = sharedContentUnit.ExpireDate?.Date ?? DateTime.MaxValue;
                if (DateTime.Now.Date <= expireDate) {
                    result = sharedContentUnit.Content;
                }
            }
            return result;
        }

    }
}
