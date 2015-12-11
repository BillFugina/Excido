using System;
using System.Linq;
using BamApps.Excido.Interface.Data;
using BamApps.Excido.Interface.Service;
using BamApps.Excido.Data.Model;

namespace BamApps.Excido.Service.Validation {
    public class CreateStampValidator<T> : ICreateStampValidator<T>, ICheckOriginal<T> where T : class, IEntity, ICreateStamp {

        private readonly IReadRepository<T> _readRepository;
        public CreateStampValidator(IReadRepository<T> readRepository) {
            this._readRepository = readRepository;
        }

        public bool ChecksWith(T entity, T original) {
            var result = original == null || entity.Created == original.Created;
            return result;
        }

        public bool IsSatisfiedBy(T entity) {
            var original = _readRepository.GetById(entity.Id);
            return ChecksWith(entity, original);

        }
    }
}
