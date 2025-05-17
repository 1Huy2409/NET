namespace QLBS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            AddColumn("dbo.Books", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Books", "CategoryId");
            AddForeignKey("dbo.Books", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
            DropColumn("dbo.Books", "Category");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "Category", c => c.String(nullable: false, maxLength: 50));
            DropForeignKey("dbo.Books", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Books", new[] { "CategoryId" });
            DropColumn("dbo.Books", "CategoryId");
            DropTable("dbo.Categories");
        }
    }
}
