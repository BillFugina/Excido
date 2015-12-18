namespace BamApps.Excido.Data.Context.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class stampsandslug : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SharedContentUnit", "Slug", c => c.String(nullable: false, maxLength: 255,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DefaultValue",
                        new AnnotationValues(oldValue: null, newValue: "NEWID()")
                    },
                }));
            AddColumn("dbo.SharedContentUnit", "Created", c => c.DateTime(nullable: false,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DefaultValue",
                        new AnnotationValues(oldValue: null, newValue: "GETUTCDATE()")
                    },
                }));
            AddColumn("dbo.SharedContentUnit", "ExpireDate", c => c.DateTime());
            AddColumn("dbo.SharedContentUnit", "ExpireCount", c => c.Int(nullable: false));
            CreateIndex("dbo.SharedContentUnit", "Slug", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.SharedContentUnit", new[] { "Slug" });
            DropColumn("dbo.SharedContentUnit", "ExpireCount");
            DropColumn("dbo.SharedContentUnit", "ExpireDate");
            DropColumn("dbo.SharedContentUnit", "Created",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DefaultValue", "GETUTCDATE()" },
                });
            DropColumn("dbo.SharedContentUnit", "Slug",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DefaultValue", "NEWID()" },
                });
        }
    }
}
