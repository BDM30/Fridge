namespace Fridge.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UPAmountDefaultAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProducts", "AmountDefault", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProducts", "AmountDefault");
        }
    }
}
