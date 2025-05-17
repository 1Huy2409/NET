namespace QLBS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateIdentity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "ImageUrl", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "ImageUrl");
        }
    }
}
