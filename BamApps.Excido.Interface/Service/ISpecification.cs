using BamApps.Excido.Interface.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BamApps.Excido.Interface.Service {
    public interface ISpecification<T> where T : IEntity {
        bool IsSatisfiedBy(T entity);
    }

}
