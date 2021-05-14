namespace StockSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtbls111 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Quantity", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "OpeningQuantity");
            DropColumn("dbo.Products", "ClosingQuantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ClosingQuantity", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "OpeningQuantity", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "Quantity");
        }
    }
}
