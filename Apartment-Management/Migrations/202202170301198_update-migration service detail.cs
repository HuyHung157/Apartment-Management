namespace Apartment_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatemigrationservicedetail : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ServiceDetails", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ServiceDetails", "Status", c => c.String());
        }
    }
}
