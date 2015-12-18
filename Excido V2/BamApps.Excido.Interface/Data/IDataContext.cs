using System;
using System.Linq;

namespace BamApps.Excido.Interface.Data {
    public interface IDataContext {
        IContextTransaction BeginTransaction();
        T GetById<T>(Guid id)
            where T : class, IEntity;
        IQueryable<T> QueryAll<T>()
            where T : class, IEntity;
        IQueryable<T> GetAll<T>()
            where T : class, IEntity;
        IQueryable<T> GetAllWhere<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression)
            where T : class, IEntity;
        IQueryable<T> QueryAllWhere<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression)
            where T : class, IEntity;
        Guid AddEntity<T>(T entity)
            where T : class, IEntity;
        void UpdateEntity<T>(T entity)
            where T : class, IEntity;
        void DeleteEntity<T>(T entity)
            where T : class, IEntity;
        int Delete<T>(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
            where T : class, IEntity;
        int SaveChanges();
    }
}