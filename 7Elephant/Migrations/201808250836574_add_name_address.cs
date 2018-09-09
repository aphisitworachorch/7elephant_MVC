namespace _7Elephant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_name_address : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "name", c => c.String());
            AddColumn("dbo.AspNetUsers", "address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "address");
            DropColumn("dbo.AspNetUsers", "name");
        }
    }
}
