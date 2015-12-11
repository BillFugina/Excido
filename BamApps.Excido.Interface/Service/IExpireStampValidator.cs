using System;
using System.Linq;
using BamApps.Excido.Interface.Data;

namespace BamApps.Excido.Interface.Service {

    

    public interface IExpireStampValidator<T> : ISpecification<T> where T : class, IEntity, IExpireStamp {
    }
}
