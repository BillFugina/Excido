using BamApps.Excido.Interface.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using BamApps.Excido.Interface.Service;
using System.Diagnostics.Contracts;
using BamApps.Excido.Service.Validation;

namespace BamApps.Excido.Service {
    public class BaseService<T> : IRepository<T> where T : class, IEntity {

        protected readonly IDataContext _dataContext;
        protected readonly IServiceValidator<T> _validator;


        public BaseService(IDataContext dataContext, IServiceValidator<T> validator) {
            Contract.Requires<ArgumentNullException>(dataContext != null, "dataContext is null.");
            Contract.Requires<ArgumentNullException>(validator != null, "validator is null.");

            _dataContext = dataContext;
            _validator = validator;
        }



        public virtual IContextTransaction BeginTransaction() {
            return _dataContext.BeginTransaction();
        }

        public virtual IQueryable<T> GetAll() {
            IQueryable<T> result = null;
            if (_validator.ValidateRead()) {
                result = _dataContext.GetAll<T>();
            }
            return result;
        }

        public virtual IQueryable<T> GetAllWhere(Expression<Func<T, bool>> expression) {
            if (_validator.ValidateWrite()) {
                return _dataContext.GetAllWhere<T>(expression);
            }
            return null;
        }

        public virtual T GetById(Guid id) {
            if (_validator.ValidateRead()) {
                return _dataContext.GetById<T>(id);
            }
            return null;
        }

        public virtual Guid AddEntity(T entity) {
            var result = Guid.Empty;

            if (_validator.ValidateWrite() && _validator.ValidateAdd(entity)) {
                result = _dataContext.AddEntity(entity);
            }
            return result;
        }

        public void UpdateEntity(T entity) {
            if (_validator.ValidateWrite() && _validator.ValidateUpdate(entity)) {
                _dataContext.UpdateEntity(entity);
            }
        }
        public virtual void DeleteEntity(T entity) {
            if (_validator.ValidateWrite() && _validator.ValidateDelete(entity)) {
                _dataContext.DeleteEntity(entity);
            }
        }

        public virtual int SaveChanges() {
            if (_validator.ValidateWrite()) {
                return _dataContext.SaveChanges();
            }
            return -1;
        }

    }
}
