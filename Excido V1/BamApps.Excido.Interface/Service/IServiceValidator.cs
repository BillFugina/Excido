using System;
using System.Linq;
using BamApps.Excido.Interface.Data;

namespace BamApps.Excido.Interface.Service {

    public interface IServiceValidator<T> where T : IEntity {
        bool ValidateRead();
        bool ValidateWrite();
        bool ValidateGet(T entity);
        bool ValidateAdd(T entity);
        bool ValidateUpdate(T entity);
        bool ValidateDelete(T entity);
    }
}
