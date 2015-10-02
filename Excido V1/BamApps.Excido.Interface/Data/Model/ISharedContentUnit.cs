namespace BamApps.Excido.Interface.Data.Model {
    public interface ISharedContentUnit : IEntity {
        string Name { get; set; }
        string Content { get; set; }
    }
}
