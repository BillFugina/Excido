using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Reflection;

namespace BamApps.Excido.Data.Extensions {
    public static class Extensions {
        /// <summary>
        /// Add all of the EntityTypeConfiguration in the specified namespace to the the ConfigurationRegistrar
        /// </summary>
        /// <param name="configuration">The ConfigurationRegistrar to which to add the mappings</param>
        /// <param name="nameSpace">Only mappings in this namespace in this assembly will be added</param>
        /// <returns>The ConfigurationRegistrar into which the mappings were added for continued fluent configuration</returns>
        public static ConfigurationRegistrar AddFromNameSpace(this ConfigurationRegistrar configuration, string nameSpace) {
            return AddFromAssembly(configuration, Assembly.GetExecutingAssembly(), nameSpace);
        }

        /// <summary>
        /// Add all of the EntityTypeConfiguration in the specified assembly and namespace to the the ConfigurationRegistrar
        /// </summary>
        /// <param name="configuration">The ConfigurationRegistrar to which to add the mappings</param>
        /// <param name="assembly">The assembly in which to look for mappings</param>
        /// <param name="nameSpace">Only mappings in this namespace in this assembly will be added</param>
        /// <returns></returns>
        public static ConfigurationRegistrar AddFromAssembly(this ConfigurationRegistrar configuration, Assembly assembly, string nameSpace) {

            assembly.GetTypes()
                .Where(type => type.Namespace == nameSpace && type.BaseType != null
                    && type.BaseType.IsGenericType
                    && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>))
                .ToList()
                .ForEach(type => {
                    dynamic instance = Activator.CreateInstance(type);
                    configuration.Add(instance);
                });

            return configuration;
        }
    }
}
