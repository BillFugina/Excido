using System;
using System.Linq;

namespace BamApps.Excido.Interface.Data {

    

    

    

    public interface IWriteRepository<in T> where T : IEntity {
        IContextTransaction BeginTransaction();

        Guid AddEntity(T entity);

        void UpdateEntity(T entity);

        void DeleteEntity(T entity);

        int SaveChanges();
    }
}