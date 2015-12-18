using BamApps.Excido.Interface.Data.Model;

namespace BamApps.Excido.Interface.Service {
    public interface ISharedContentServiceValidator<T> where T : ISharedContentUnit {
        bool ValidateGetSlug(T sharedContentUnit);
    }
}