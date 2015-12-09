using System;

namespace BamApps.Excido.Interface.Data.Model {
    public interface ISharedContentUnit {
        string Content { get; set; }
        DateTime Created { get; set; }
        int ExpireCount { get; set; }
        DateTime? ExpireDate { get; set; }
        Guid Id { get; set; }
        string Name { get; set; }
        string Slug { get; set; }
    }
}