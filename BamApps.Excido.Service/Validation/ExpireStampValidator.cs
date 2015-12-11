using System;
using System.Linq;
using BamApps.Excido.Interface.Data;
using BamApps.Excido.Interface.Service;

namespace BamApps.Excido.Service.Validation {

    public class ExpireStampValidator<T> : ISpecification<T>, IExpireStampValidator<T> where T : class, IEntity, IExpireStamp {
        public ExpireStampValidator() {
        }

        public bool IsSatisfiedBy(T entity) {
            var result = false;
            if (entity != null) {
                var expireDate = entity.ExpireDate?.Date ?? DateTime.MaxValue;
                result = DateTime.Now.Date <= expireDate;
            }
            return result;
        }
    }
}
