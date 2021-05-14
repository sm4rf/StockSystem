namespace StockSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtbls1111 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Orders", "EmployeeId");
            CreateIndex("dbo.Orders", "ProductId");
            AddForeignKey("dbo.Orders", "EmployeeId", "dbo.Employees", "EmployeeId", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Orders", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Orders", new[] { "ProductId" });
            DropIndex("dbo.Orders", new[] { "EmployeeId" });
        }
    }
}
