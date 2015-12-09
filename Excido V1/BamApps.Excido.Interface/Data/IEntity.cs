using System;

namespace BamApps.Excido.Interface.Data {
    public interface IEntity {
        Guid Id { get; set; }
    }

    public interface ICreateStamp {
        DateTime Created { get; set; }
    }
}