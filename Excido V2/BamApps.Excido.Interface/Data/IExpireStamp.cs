using System;

namespace BamApps.Excido.Interface.Data {

    public interface IExpireStamp {
        DateTime? ExpireDate { get; set; }
    }
}