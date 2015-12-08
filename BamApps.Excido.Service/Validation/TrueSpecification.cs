using BamApps.Excido.Interface.Data;
using BamApps.Excido.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BamApps.Excido.Service.Validation {
    public class TrueSpecification<T> : ISpecification<T> where T : IEntity {
        public bool IsSatisfiedBy(T entity) {
            return true;
        }
    }
}
