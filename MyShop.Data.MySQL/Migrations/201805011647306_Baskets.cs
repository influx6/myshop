namespace MyShop.Data.MySQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Baskets : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Baskets",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BasketItems",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        BasketID = c.String(maxLength: 128),
                        ProductID = c.String(),
                        Quantity = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Baskets", t => t.BasketID)
                .Index(t => t.BasketID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BasketItems", "BasketID", "dbo.Baskets");
            DropIndex("dbo.BasketItems", new[] { "BasketID" });
            DropTable("dbo.BasketItems");
            DropTable("dbo.Baskets");
        }
    }
}
