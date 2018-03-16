namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRentals : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "Rental_Id", "dbo.Rentals");
            DropForeignKey("dbo.Movies", "Rental_Id", "dbo.Rentals");
            DropIndex("dbo.Customers", new[] { "Rental_Id" });
            DropIndex("dbo.Movies", new[] { "Rental_Id" });
            AddColumn("dbo.Rentals", "Customer_Id", c => c.Int(nullable: false));
            AddColumn("dbo.Rentals", "Movie_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Rentals", "DateReturned", c => c.DateTime());
            CreateIndex("dbo.Rentals", "Customer_Id");
            CreateIndex("dbo.Rentals", "Movie_Id");
            AddForeignKey("dbo.Rentals", "Customer_Id", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Rentals", "Movie_Id", "dbo.Movies", "Id", cascadeDelete: true);
            DropColumn("dbo.Customers", "Rental_Id");
            DropColumn("dbo.Movies", "Rental_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "Rental_Id", c => c.Int());
            AddColumn("dbo.Customers", "Rental_Id", c => c.Int());
            DropForeignKey("dbo.Rentals", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.Rentals", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Rentals", new[] { "Movie_Id" });
            DropIndex("dbo.Rentals", new[] { "Customer_Id" });
            AlterColumn("dbo.Rentals", "DateReturned", c => c.DateTime(nullable: false));
            DropColumn("dbo.Rentals", "Movie_Id");
            DropColumn("dbo.Rentals", "Customer_Id");
            CreateIndex("dbo.Movies", "Rental_Id");
            CreateIndex("dbo.Customers", "Rental_Id");
            AddForeignKey("dbo.Movies", "Rental_Id", "dbo.Rentals", "Id");
            AddForeignKey("dbo.Customers", "Rental_Id", "dbo.Rentals", "Id");
        }
    }
}
