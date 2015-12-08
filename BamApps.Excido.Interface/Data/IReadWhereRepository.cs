using System;
using System.Linq;

namespace BamApps.Excido.Interface.Data {
    public interface IReadWhereRepository<T> where T : class, IEntity {
        IQueryable<T> GetAllWhere(System.Linq.Expressions.Expression<Func<T, bool>> expression);
        //IQueryable<T> QueryAllWhere(System.Linq.Expressions.Expression<Func<T, bool>> expression);
    }
}