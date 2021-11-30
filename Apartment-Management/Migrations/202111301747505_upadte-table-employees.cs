namespace Apartment_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upadtetableemployees : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "Role", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "Role", c => c.String());
        }
    }
}
