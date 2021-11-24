namespace Apartment_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateServiceTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ServiceDetails", "Apartment_ApartmentID", "dbo.Apartments");
            DropIndex("dbo.ServiceDetails", new[] { "Apartment_ApartmentID" });
            DropColumn("dbo.ServiceDetails", "ApartmentID");
            RenameColumn(table: "dbo.ServiceDetails", name: "Apartment_ApartmentID", newName: "ApartmentID");
            CreateTable(
                "dbo.ServiceTypes",
                c => new
                    {
                        ServiceTypeID = c.Int(nullable: false, identity: true),
                        ServiceTypeName = c.String(),
                        UnitPrice = c.Double(nullable: false),
                        Unit = c.String(),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsArchive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ServiceTypeID);
            
            AddColumn("dbo.ServiceDetails", "ServiceTypeID", c => c.Int(nullable: false));
            AlterColumn("dbo.ServiceDetails", "ApartmentID", c => c.Int(nullable: false));
            AlterColumn("dbo.ServiceDetails", "ApartmentID", c => c.Int(nullable: false));
            CreateIndex("dbo.ServiceDetails", "ApartmentID");
            CreateIndex("dbo.ServiceDetails", "ServiceTypeID");
            AddForeignKey("dbo.ServiceDetails", "ServiceTypeID", "dbo.ServiceTypes", "ServiceTypeID", cascadeDelete: true);
            AddForeignKey("dbo.ServiceDetails", "ApartmentID", "dbo.Apartments", "ApartmentID", cascadeDelete: true);
            DropColumn("dbo.ServiceDetails", "ServiceType");
            DropColumn("dbo.ServiceDetails", "UnitPrice");
            DropColumn("dbo.ServiceDetails", "Unit");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ServiceDetails", "Unit", c => c.String());
            AddColumn("dbo.ServiceDetails", "UnitPrice", c => c.Double(nullable: false));
            AddColumn("dbo.ServiceDetails", "ServiceType", c => c.String());
            DropForeignKey("dbo.ServiceDetails", "ApartmentID", "dbo.Apartments");
            DropForeignKey("dbo.ServiceDetails", "ServiceTypeID", "dbo.ServiceTypes");
            DropIndex("dbo.ServiceDetails", new[] { "ServiceTypeID" });
            DropIndex("dbo.ServiceDetails", new[] { "ApartmentID" });
            AlterColumn("dbo.ServiceDetails", "ApartmentID", c => c.Int());
            AlterColumn("dbo.ServiceDetails", "ApartmentID", c => c.String());
            DropColumn("dbo.ServiceDetails", "ServiceTypeID");
            DropTable("dbo.ServiceTypes");
            RenameColumn(table: "dbo.ServiceDetails", name: "ApartmentID", newName: "Apartment_ApartmentID");
            AddColumn("dbo.ServiceDetails", "ApartmentID", c => c.String());
            CreateIndex("dbo.ServiceDetails", "Apartment_ApartmentID");
            AddForeignKey("dbo.ServiceDetails", "Apartment_ApartmentID", "dbo.Apartments", "ApartmentID");
        }
    }
}
