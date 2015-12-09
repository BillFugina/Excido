using BamApps.Excido.Interface.Data;
using BamApps.Excido.Interface.Data.Model;
using BamApps.Excido.Interface.Service;

namespace BamApps.Excido.Service {
    public interface IReadSharedContentPredicate<T> : IPredicate where T : IEntity, ISharedContentUnit {
    }

    public interface IWriteSharedContentPredicate<T> : IPredicate where T : IEntity, ISharedContentUnit {
    }

    public interface IGetSharedContentValidator<T> : ISpecification<T> where T : IEntity, ISharedContentUnit {
    }

    public interface IAddSharedContentValidator<T> : ISpecification<T> where T : IEntity, ISharedContentUnit {
    }

    public interface IUpdateSharedContentValidator<T> : ISpecification<T> where T : IEntity, ISharedContentUnit {
    }

    public interface IDeleteSharedContentValidator<T> : ISpecification<T> where T : IEntity, ISharedContentUnit {
    }


}