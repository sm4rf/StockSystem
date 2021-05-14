namespace StockSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpfd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "FilePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "FilePath");
        }
    }
}
