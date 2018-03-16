namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRentals : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rentals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateRented = c.DateTime(nullable: false),
                        DateReturned = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Customers", "Rental_Id", c => c.Int());
            AddColumn("dbo.Movies", "Rental_Id", c => c.Int());
            CreateIndex("dbo.Customers", "Rental_Id");
            CreateIndex("dbo.Movies", "Rental_Id");
            AddForeignKey("dbo.Customers", "Rental_Id", "dbo.Rentals", "Id");
            AddForeignKey("dbo.Movies", "Rental_Id", "dbo.Rentals", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "Rental_Id", "dbo.Rentals");
            DropForeignKey("dbo.Customers", "Rental_Id", "dbo.Rentals");
            DropIndex("dbo.Movies", new[] { "Rental_Id" });
            DropIndex("dbo.Customers", new[] { "Rental_Id" });
            DropColumn("dbo.Movies", "Rental_Id");
            DropColumn("dbo.Customers", "Rental_Id");
            DropTable("dbo.Rentals");
        }
    }
}
