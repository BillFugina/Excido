using System;
using System.Linq;

namespace BamApps.Excido.Interface.Data {

    public interface IRepository<T> : IReadRepository<T>, IReadWhereRepository<T>, IWriteRepository<T> where T : class, IEntity  {

    }
}