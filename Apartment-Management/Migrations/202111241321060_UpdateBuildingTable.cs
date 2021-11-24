namespace Apartment_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBuildingTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Buildings", "BuildingName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Buildings", "BuildingName");
        }
    }
}
