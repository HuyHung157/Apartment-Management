namespace Apartment_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatelogin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "loginerror", c => c.String());
            AlterColumn("dbo.Employees", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "Password", c => c.String());
            DropColumn("dbo.Employees", "loginerror");
        }
    }
}
