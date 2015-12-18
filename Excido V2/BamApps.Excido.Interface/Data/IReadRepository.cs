using System;
using System.Linq;

namespace BamApps.Excido.Interface.Data {



    public interface IReadRepository<out T> where T : IEntity {
        T GetById(Guid id);

        //IQueryable<T> QueryAll();

        IQueryable<T> GetAll();
    }
}