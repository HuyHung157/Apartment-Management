namespace Apartment_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removefieldloginerror : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Employees", "loginerror");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "loginerror", c => c.String());
        }
    }
}
