using BamApps.Excido.Interface.Data;
using BamApps.Excido.Interface.Data.Model;
using System;
namespace BamApps.Excido.Data.Model {

    public class SharedContentUnit : IEntity, ICreateStamp, ISharedContentUnit, IExpireStamp {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Slug { get; set; }
        public DateTime Created { get; set; }
        public DateTime? ExpireDate { get; set; }
        public int ExpireCount { get; set; }

    }
}
