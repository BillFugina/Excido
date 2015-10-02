using System;

namespace BamApps.Excido.Interface.Data {
    public interface IContextTransaction : IDisposable {
        void Commit();
        void Rollback();
    }
}
