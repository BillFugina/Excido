using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using BamApps.Excido.Data.Model;
using BamApps.Excido.Interface.Data;
using BamApps.Excido.Interface.Service;
using BamApps.Excido.Service.Validation;

namespace BamApps.Excido.Service {

    public class SharedContentService : BaseService<SharedContentUnit>, ISharedContentService {

        /// <summary>
        /// Initializes a new instance of the <see cref="SharedContent"/> class.
        /// </summary>
        /// <param name="dataContext"></param>
        public SharedContentService(IDataContext dataContext) : base(dataContext) {
            var sharedContentCreateStampValidator = new SharedContentCreateStampValidator(this);
            var sharedContentExpireDataValidator = new SharedContentExpireDataValidator(this);

            var addValidator = sharedContentCreateStampValidator.And(sharedContentExpireDataValidator);
            var updateValidator = sharedContentCreateStampValidator.And(sharedContentExpireDataValidator);

            _addValidation = addValidator;
            _updateValidation = updateValidator;
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



    class SharedContentCreateStampValidator : ISpecification<SharedContentUnit> {
        private readonly IReadRepository<SharedContentUnit> _readRepository;

        public SharedContentCreateStampValidator(IReadRepository<SharedContentUnit> readRepository) {
            Contract.Requires(readRepository != null);
            this._readRepository = readRepository;
        }

        public bool IsSatisfiedBy(SharedContentUnit entity) {
            var existing = _readRepository.GetById(entity.Id);

            if (existing != null) {
                entity.Created = existing.Created;
            }
            else {
                entity.Created = DateTime.Now;
            }

            return true;
        }
    }

    class SharedContentExpireDataValidator : ISpecification<SharedContentUnit> {
        private readonly IReadRepository<SharedContentUnit> _readRepository;

        public SharedContentExpireDataValidator(IReadRepository<SharedContentUnit> _readRepository) {
            Contract.Requires(_readRepository != null);

            this._readRepository = _readRepository;
        }

        public bool IsSatisfiedBy(SharedContentUnit entity) {
            var existing = _readRepository.GetById(entity.Id);
            var result = (existing == null || entity.ExpireDate == existing.ExpireDate || entity.ExpireDate >= DateTime.Now);
            return result;
        }
    }

}
