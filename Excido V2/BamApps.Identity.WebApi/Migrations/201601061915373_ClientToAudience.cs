namespace BamApps.Identity.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClientToAudience : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "AudienceId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Clients", "AudienceId");
            AddForeignKey("dbo.Clients", "AudienceId", "dbo.Audiences", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clients", "AudienceId", "dbo.Audiences");
            DropIndex("dbo.Clients", new[] { "AudienceId" });
            DropColumn("dbo.Clients", "AudienceId");
        }
    }
}
