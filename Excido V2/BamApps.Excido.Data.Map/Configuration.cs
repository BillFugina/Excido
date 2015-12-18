using System.Data.Entity.ModelConfiguration.Configuration;
using BamApps.Excido.Data.Extensions;
using System.Reflection;

namespace BamApps.Excido.Data.Map {
    public static class Configuration {
        public static void Configure(ConfigurationRegistrar registrar) {
            registrar.AddFromAssembly(Assembly.GetExecutingAssembly(), "BamApps.Excido.Data.Map");
        }
    }

}
