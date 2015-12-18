using System;
using System.Linq;
using BamApps.Excido.Data.Model;

namespace BamApps.Excido.Service.Validation {

    public class SharedContentCreateStampValidator : CreateStampValidator<SharedContentUnit> {
        public SharedContentCreateStampValidator(Interface.Data.IReadRepository<SharedContentUnit> readRepository) : base(readRepository) {
        }
    }
}
