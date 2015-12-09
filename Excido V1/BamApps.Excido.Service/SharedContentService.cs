using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using BamApps.Excido.Data.Model;
using BamApps.Excido.Interface.Data;
using BamApps.Excido.Interface.Service;
using BamApps.Excido.Service.Validation;

namespace BamApps.Excido.Service {

    public class SharedContentService : BaseService<SharedContentUnit>, ISharedContentService<SharedContentUnit> {
        private readonly ISharedContentServiceValidator<SharedContentUnit> _sharedConentServiceValidator;

        public SharedContentService(IDataContext dataContext, 
            IServiceValidator<SharedContentUnit> validator, 
            ISharedContentServiceValidator<SharedContentUnit> sharedContentServiceValidator) 
            : base(dataContext, validator) {
            Contract.Requires<ArgumentNullException>(sharedContentServiceValidator != null, "sharedContentServiceValidator is null.");
            _sharedConentServiceValidator = sharedContentServiceValidator;
        }

        public string GetSlugContent(string slug) {
            var result = "";

            if (_validator.ValidateRead()) {

                var sharedContentUnit = GetSharedContentUnit(slug);

                if (_sharedConentServiceValidator.ValidateGetSlug(sharedContentUnit)) {
                    result = sharedContentUnit.Content;
                }

            }
            return result;
        }

        public virtual SharedContentUnit GetSharedContentUnit(string slug) {
            return _dataContext.QueryAllWhere<SharedContentUnit>(s => s.Slug == slug).SingleOrDefault();
        }
    }

    public class SharedContentRepository : BaseService<SharedContentUnit> {
        public SharedContentRepository(IDataContext dataContext) : base(dataContext, NullServiceValidator<SharedContentUnit>.Instance()) {
        }
    }

}
