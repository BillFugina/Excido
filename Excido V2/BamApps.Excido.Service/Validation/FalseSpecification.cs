using System;
using System.Linq;
using BamApps.Excido.Interface.Data;
using BamApps.Excido.Interface.Service;

namespace BamApps.Excido.Service.Validation {
    public class FalseSpecification<T> : ISpecification<T> where T : IEntity {
        public bool IsSatisfiedBy(T entity) {
            return false;
        }
    }
}
