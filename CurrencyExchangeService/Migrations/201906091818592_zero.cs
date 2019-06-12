namespace CurrencyExchangeService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zero : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Code = c.String(nullable: false, maxLength: 50),
                        isDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CurrencyPairs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        BaseCurrencyID = c.Int(),
                        ChildCurrencyID = c.Int(),
                        isDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Currencies", t => t.BaseCurrencyID)
                .ForeignKey("dbo.Currencies", t => t.ChildCurrencyID)
                .Index(t => t.BaseCurrencyID)
                .Index(t => t.ChildCurrencyID);
            
            CreateTable(
                "dbo.RateHistories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CurrencyPairID = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        UpdateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CurrencyPairs", t => t.CurrencyPairID, cascadeDelete: true)
                .Index(t => t.CurrencyPairID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RateHistories", "CurrencyPairID", "dbo.CurrencyPairs");
            DropForeignKey("dbo.CurrencyPairs", "ChildCurrencyID", "dbo.Currencies");
            DropForeignKey("dbo.CurrencyPairs", "BaseCurrencyID", "dbo.Currencies");
            DropIndex("dbo.RateHistories", new[] { "CurrencyPairID" });
            DropIndex("dbo.CurrencyPairs", new[] { "ChildCurrencyID" });
            DropIndex("dbo.CurrencyPairs", new[] { "BaseCurrencyID" });
            DropTable("dbo.RateHistories");
            DropTable("dbo.CurrencyPairs");
            DropTable("dbo.Currencies");
        }
    }
}
