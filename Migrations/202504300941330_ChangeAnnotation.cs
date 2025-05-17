namespace QLBS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAnnotation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Users", "UserName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "UserName", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
