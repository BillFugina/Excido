namespace BamApps.Excido.Data.Context.Migrations {
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Model;
    using System.Data.Entity.SqlServer;
    using System.Diagnostics;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BamApps.Excido.Data.Context.ExcidoContext> {
        public Configuration() {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("System.Data.SqlClient", new CustomSqlServerMigrationSqlGenerator());
        }

        protected override void Seed(BamApps.Excido.Data.Context.ExcidoContext context) {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }

    internal class CustomSqlServerMigrationSqlGenerator : SqlServerMigrationSqlGenerator {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomSqlServerMigrationSqlGenerator"/> class.
        /// </summary>
        public CustomSqlServerMigrationSqlGenerator() : base() {
            Debugger.Launch();
        }

        protected override void Generate(AddColumnOperation addColumnOperation) {
            SetCreatedUtcColumn(addColumnOperation.Column);

            base.Generate(addColumnOperation);
        }

        protected override void Generate(CreateTableOperation createTableOperation) {
            SetCreatedUtcColumn(createTableOperation.Columns);

            base.Generate(createTableOperation);
        }

        private static void SetCreatedUtcColumn(IEnumerable<ColumnModel> columns) {
            foreach (var columnModel in columns) {
                SetCreatedUtcColumn(columnModel);
            }
        }

        private static void SetCreatedUtcColumn(PropertyModel column) {
            var columnModel = column as ColumnModel;
            if (columnModel != null) {
                if (columnModel.Annotations.ContainsKey("DefaultValue")) {
                    var defaultValue = columnModel.Annotations["DefaultValue"].NewValue.ToString();
                    column.DefaultValueSql = defaultValue;
                    columnModel.Annotations.Remove("DefaultValue");
                }

            }
        }
    }
}
