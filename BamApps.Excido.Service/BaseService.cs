using BamApps.Excido.Interface.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace BamApps.Excido.Service {
    public class BaseService<T> : IRepository<T> where T : class, IEntity {

        protected readonly IDataContext _dataContext;
        public BaseService(IDataContext dataContext) {
            _dataContext = dataContext;
        }

        public virtual Guid AddEntity(T entity) {
            return _dataContext.AddEntity(entity);
        }

        public virtual IContextTransaction BeginTransaction() {
            return _dataContext.BeginTransaction();
        }

        public virtual void DeleteEntity(T entity) {
            _dataContext.DeleteEntity(entity);
        }

        public virtual IQueryable<T> GetAll() {
            return _dataContext.GetAll<T>();
        }

        public virtual IQueryable<T> GetAllWhere(Expression<Func<T, bool>> expression) {
            return _dataContext.GetAllWhere<T>(expression);
        }

        public virtual T GetById(Guid id) {
            return _dataContext.GetById<T>(id);
        }

        public virtual IQueryable<T> QueryAll() {
            return _dataContext.QueryAll<T>();
        }

        public virtual IQueryable<T> QueryAllWhere(Expression<Func<T, bool>> expression) {
            return _dataContext.QueryAllWhere<T>(expression);
        }

        public virtual int SaveChanges() {
            return _dataContext.SaveChanges();
        }

        public void UpdateEntity(T entity) {
            _dataContext.UpdateEntity(entity);
        }
    }
}
