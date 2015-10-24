namespace Fridge.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DomainExpanded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Fridges",
                c => new
                    {
                        FridgeID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.FridgeID);
            
            CreateTable(
                "dbo.ImportanceLevels",
                c => new
                    {
                        ImportanceLevelID = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ImportanceLevelID);
            
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        IngredientID = c.Int(nullable: false, identity: true),
                        CategoryID = c.Int(nullable: false),
                        RecipeID = c.Int(nullable: false),
                        ImportanceLevelID = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IngredientID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        CategoryID = c.Int(nullable: false),
                        Name = c.String(),
                        Barcode = c.String(),
                        AmountDefault = c.Int(nullable: false),
                        UnitMeasureID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID);
            
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        RecipeID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ProcessDescription = c.String(),
                    })
                .PrimaryKey(t => t.RecipeID);
            
            CreateTable(
                "dbo.RecipeTags",
                c => new
                    {
                        RecipeTagID = c.Int(nullable: false, identity: true),
                        RecipeID = c.Int(nullable: false),
                        TagID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RecipeTagID);
            
            CreateTable(
                "dbo.ReplaceOptions",
                c => new
                    {
                        ReplaceOptionID = c.Int(nullable: false, identity: true),
                        IngredientOneID = c.Int(nullable: false),
                        IngredientTwoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReplaceOptionID);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.TagID);
            
            CreateTable(
                "dbo.UnitMeasures",
                c => new
                    {
                        UnitMeasureID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ShortName = c.String(),
                    })
                .PrimaryKey(t => t.UnitMeasureID);
            
            CreateTable(
                "dbo.UserProducts",
                c => new
                    {
                        UserProductID = c.Int(nullable: false, identity: true),
                        FridgeID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                        Name = c.String(),
                        UnitMeasureID = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        ExpirationDate = c.String(),
                    })
                .PrimaryKey(t => t.UserProductID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserProducts");
            DropTable("dbo.UnitMeasures");
            DropTable("dbo.Tags");
            DropTable("dbo.ReplaceOptions");
            DropTable("dbo.RecipeTags");
            DropTable("dbo.Recipes");
            DropTable("dbo.Products");
            DropTable("dbo.Ingredients");
            DropTable("dbo.ImportanceLevels");
            DropTable("dbo.Fridges");
            DropTable("dbo.Categories");
        }
    }
}
