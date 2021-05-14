namespace StockSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtbls1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "Position", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "Position", c => c.String());
        }
    }
}
