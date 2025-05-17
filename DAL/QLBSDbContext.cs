using QLBS.DAL;
using QLBS.DAL.Entities;
using QLBS.Migrations;
using System;
using System.Data.Entity;
using System.Linq;

namespace QLBS
{
    public class QLBSDbContext : DbContext
    {
        // Your context has been configured to use a 'QLBS' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'QLBS.QLBS' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'QLBS' 
        // connection string in the application configuration file.
        public QLBSDbContext()   
            : base("name=QLBS")
        {
            // thiết lập migration
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<QLBSDbContext, Configuration>());
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}