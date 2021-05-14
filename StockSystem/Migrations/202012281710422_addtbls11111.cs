namespace StockSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtbls11111 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Orders", new[] { "EmployeeId" });
            DropColumn("dbo.Orders", "EmployeeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "EmployeeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "EmployeeId");
            AddForeignKey("dbo.Orders", "EmployeeId", "dbo.Employees", "EmployeeId", cascadeDelete: true);
        }
    }
}
