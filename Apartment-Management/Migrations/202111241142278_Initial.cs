namespace Apartment_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Apartments",
                c => new
                    {
                        ApartmentID = c.Int(nullable: false, identity: true),
                        BuildingID = c.Int(nullable: false),
                        ApartmentName = c.String(),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsArchive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ApartmentID)
                .ForeignKey("dbo.Buildings", t => t.BuildingID, cascadeDelete: true)
                .Index(t => t.BuildingID);
            
            CreateTable(
                "dbo.Buildings",
                c => new
                    {
                        BuildingID = c.Int(nullable: false, identity: true),
                        BranchID = c.Int(nullable: false),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsArchive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BuildingID)
                .ForeignKey("dbo.Branches", t => t.BranchID, cascadeDelete: true)
                .Index(t => t.BranchID);
            
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        BranchID = c.Int(nullable: false, identity: true),
                        BranchName = c.String(),
                        Address = c.String(),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsArchive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BranchID);
            
            CreateTable(
                "dbo.BuildingEmployees",
                c => new
                    {
                        BuildingEmployeesID = c.Int(nullable: false, identity: true),
                        BuildingID = c.Int(nullable: false),
                        EmployeeID = c.Int(nullable: false),
                        RoleInBuilding = c.String(),
                        OfficeName = c.String(),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsArchive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BuildingEmployeesID)
                .ForeignKey("dbo.Buildings", t => t.BuildingID, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.EmployeeID, cascadeDelete: true)
                .Index(t => t.BuildingID)
                .Index(t => t.EmployeeID);
            
            CreateTable(
                "dbo.ServiceDetails",
                c => new
                    {
                        ServiceDetailID = c.Int(nullable: false, identity: true),
                        ApartmentID = c.String(),
                        Status = c.String(),
                        ServiceType = c.String(),
                        UnitPrice = c.Double(nullable: false),
                        Unit = c.String(),
                        Quantity = c.Double(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsArchive = c.Boolean(nullable: false),
                        Apartment_ApartmentID = c.Int(),
                    })
                .PrimaryKey(t => t.ServiceDetailID)
                .ForeignKey("dbo.Apartments", t => t.Apartment_ApartmentID)
                .Index(t => t.Apartment_ApartmentID);
            
            CreateTable(
                "dbo.ApartmentDetails",
                c => new
                    {
                        ApartmentDetailID = c.Int(nullable: false, identity: true),
                        ApartmentID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        IsHost = c.Boolean(nullable: false),
                        Relationship = c.String(),
                        Type = c.String(),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsArchive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ApartmentDetailID)
                .ForeignKey("dbo.Apartments", t => t.ApartmentID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.ApartmentID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        ApartmentDetailID = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Dob = c.DateTime(nullable: false),
                        PhoneNumber = c.Int(nullable: false),
                        IdCard = c.Int(nullable: false),
                        Address = c.String(),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsArchive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        Role = c.String(),
                        Username = c.String(),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Dob = c.DateTime(nullable: false),
                        PhoneNumber = c.Int(nullable: false),
                        IdCard = c.Int(nullable: false),
                        Address = c.String(),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsArchive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BuildingEmployees", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.ApartmentDetails", "UserID", "dbo.Users");
            DropForeignKey("dbo.ApartmentDetails", "ApartmentID", "dbo.Apartments");
            DropForeignKey("dbo.ServiceDetails", "Apartment_ApartmentID", "dbo.Apartments");
            DropForeignKey("dbo.BuildingEmployees", "BuildingID", "dbo.Buildings");
            DropForeignKey("dbo.Buildings", "BranchID", "dbo.Branches");
            DropForeignKey("dbo.Apartments", "BuildingID", "dbo.Buildings");
            DropIndex("dbo.ApartmentDetails", new[] { "UserID" });
            DropIndex("dbo.ApartmentDetails", new[] { "ApartmentID" });
            DropIndex("dbo.ServiceDetails", new[] { "Apartment_ApartmentID" });
            DropIndex("dbo.BuildingEmployees", new[] { "EmployeeID" });
            DropIndex("dbo.BuildingEmployees", new[] { "BuildingID" });
            DropIndex("dbo.Buildings", new[] { "BranchID" });
            DropIndex("dbo.Apartments", new[] { "BuildingID" });
            DropTable("dbo.Employees");
            DropTable("dbo.Users");
            DropTable("dbo.ApartmentDetails");
            DropTable("dbo.ServiceDetails");
            DropTable("dbo.BuildingEmployees");
            DropTable("dbo.Branches");
            DropTable("dbo.Buildings");
            DropTable("dbo.Apartments");
        }
    }
}
