namespace BamApps.Excido.Interface.Data.Model {
    public interface ISharedContentUnit : IEntity {
        string Name { get; set; }
        string Content { get; set; }
        string Slug { get; set; }
        System.DateTime Created { get; set; }
        System.DateTime? ExpireDate { get; set; }
        int ExpireCount { get; set; }
    }
}
