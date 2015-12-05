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

    public interface IRepository<T> : IReadRepository<T>, IReadWhereRepository<T>, IWriteRepository<T> where T : class, IEntity  {

    }

    public interface IReadRepository<out T> where T : IEntity {
        T GetById(Guid id);

        IQueryable<T> QueryAll();

        IQueryable<T> GetAll();
    }

    public interface IReadWhereRepository<T> where T : class, IEntity {
        IQueryable<T> GetAllWhere(System.Linq.Expressions.Expression<Func<T, bool>> expression);
        IQueryable<T> QueryAllWhere(System.Linq.Expressions.Expression<Func<T, bool>> expression);
    }

    public interface IWriteRepository<in T> where T : IEntity {
        IContextTransaction BeginTransaction();

        Guid AddEntity(T entity);

        void UpdateEntity(T entity);

        void DeleteEntity(T entity);

        int SaveChanges();
    }
}