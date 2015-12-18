using BamApps.Excido.Data.Model;
using BamApps.Excido.Interface.Data;
using EntityFramework.Extensions;
using System;
using System.Data.Entity;
using System.Linq;

namespace BamApps.Excido.Data.Context {
    public class ExcidoContext : DbContext, IDataContext {

        public DbSet<SharedContentUnit> SharedContentUnits {
            get {
                return Set<SharedContentUnit>();
            }
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            Map.Configuration.Configure(modelBuilder.Configurations);
        }

        public T GetById<T>(Guid id) where T : class, IEntity {
            return Set<T>().Find(id);
        }

        public IQueryable<T> QueryAll<T>() where T : class, IEntity {
            return Set<T>().AsNoTracking();
        }

        public IQueryable<T> GetAll<T>() where T : class, IEntity {
            return Set<T>();
        }

        public IQueryable<T> GetAllWhere<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : class, IEntity {
            return Set<T>().Where(expression);
        }

        public IQueryable<T> QueryAllWhere<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : class, IEntity {
            return Set<T>().Where(expression).AsNoTracking();
        }

        public Guid AddEntity<T>(T entity) where T : class, IEntity {
            if (entity.Id == Guid.Empty) {
                entity.Id = Guid.NewGuid();
            }
            Set<T>().Add(entity);
            return entity.Id;
        }

        public void DeleteEntity<T>(T entity) where T : class, IEntity {
            var found = Set<T>().Find(entity.Id);
            if (found != null) {
                Set<T>().Remove(found);
            }
        }

        public int Delete<T>(System.Linq.Expressions.Expression<Func<T, bool>> predicate) where T : class, IEntity {
            return Set<T>().Where(predicate).Delete();
        }

        public IContextTransaction BeginTransaction() {
            return new ContextTransaction(Database.BeginTransaction());
        }

        void IDataContext.UpdateEntity<T>(T entity) {
            var entry = ChangeTracker.Entries<T>().FirstOrDefault(e => e.Entity.Id == entity.Id);
            if (entry != null) {
                entry.CurrentValues.SetValues(entity);
            }
            else {
                Set<T>().Attach(entity);

                entry = ChangeTracker.Entries<T>().FirstOrDefault(e => e.Entity == entity);
                if (entry != null) {
                    entry.State = EntityState.Modified;
                }
            }
        }
    }
}
