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

        protected IPredicate _readValidation = Predicate.True;
        protected IPredicate _writeValidation = Predicate.True;
        protected ISpecification<T> _addValidation = Specification<T>.True;
        protected ISpecification<T> _updateValidation = Specification<T>.True;
        protected ISpecification<T> _deleteValidation = Specification<T>.True;

        protected readonly IDataContext _dataContext;
        public BaseService(IDataContext dataContext) {
            Contract.Requires(dataContext != null, "dataContext is null.");
            _dataContext = dataContext;
        }

        public BaseService(IDataContext dataContext, IPredicate readValidation, IPredicate writeValidation, ISpecification<T> addValidation, ISpecification<T> updateValidation, ISpecification<T> deleteValidation) : this(dataContext) {
            Contract.Requires(readValidation != null);
            Contract.Requires(writeValidation != null);
            Contract.Requires(addValidation != null);
            Contract.Requires(updateValidation != null);
            Contract.Requires(deleteValidation != null);

            _readValidation = readValidation;
            _writeValidation = writeValidation;
            _addValidation = addValidation;
            _updateValidation = updateValidation;
            _deleteValidation = deleteValidation;
        }


        public virtual IContextTransaction BeginTransaction() {
            return _dataContext.BeginTransaction();
        }

        public virtual IQueryable<T> GetAll() {
            IQueryable<T> result = null;
            if (_readValidation.Test()) {
                result = _dataContext.GetAll<T>();
            }
            return result;
        }

        public virtual IQueryable<T> GetAllWhere(Expression<Func<T, bool>> expression) {
            if (_readValidation.Test()) {
                return _dataContext.GetAllWhere<T>(expression);
            }
            return null;
        }

        public virtual T GetById(Guid id) {
            if (_readValidation.Test()) {
                return _dataContext.GetById<T>(id);
            }
            return null;
        }

        public virtual Guid AddEntity(T entity) {
            var result = Guid.Empty;

            if (_writeValidation.Test() && _addValidation.IsSatisfiedBy(entity)) {
                result = _dataContext.AddEntity(entity);
            }
            return result;
        }

        public void UpdateEntity(T entity) {
            if (_writeValidation.Test() && _updateValidation.IsSatisfiedBy(entity)) {
                _dataContext.UpdateEntity(entity);
            }
        }
        public virtual void DeleteEntity(T entity) {
            if (_writeValidation.Test() && _deleteValidation.IsSatisfiedBy(entity)) {
                _dataContext.DeleteEntity(entity);
            }
        }

        public virtual int SaveChanges() {
            if (_writeValidation.Test()) {
                return _dataContext.SaveChanges();
            }
            return -1;
        }

    }
}
