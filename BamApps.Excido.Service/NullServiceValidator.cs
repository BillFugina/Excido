using System;
using System.Linq;
using BamApps.Excido.Interface.Data;
using BamApps.Excido.Interface.Service;

namespace BamApps.Excido.Service {

    public class NullServiceValidator<T> : IServiceValidator<T> where T :IEntity {
        public static NullServiceValidator<T> Instance() {
            return new NullServiceValidator<T>();
        }

        public bool ValidateAdd(T entity) {
            return true;
        }

        public bool ValidateDelete(T entity) {
            return true;
        }

        public bool ValidateGet(T entity) {
            return true;
        }

        public bool ValidateRead() {
            return true;
        }

        public bool ValidateUpdate(T entity) {
            return true;
        }

        public bool ValidateWrite() {
            return true;
        }
    }

}

