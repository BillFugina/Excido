namespace BamApps.Identity.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Audiences : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Audiences",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Secret = c.String(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Audiences");
        }
    }
}
