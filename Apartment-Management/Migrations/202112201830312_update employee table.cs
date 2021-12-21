namespace Apartment_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateemployeetable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "Username", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "Username", c => c.String());
        }
    }
}
