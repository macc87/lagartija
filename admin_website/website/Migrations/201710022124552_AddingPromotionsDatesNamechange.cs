namespace website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingPromotionsDatesNamechange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Promotions", "InitialDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Promotions", "FinalDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Promotions", "InitialTime");
            DropColumn("dbo.Promotions", "FinalTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Promotions", "FinalTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Promotions", "InitialTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Promotions", "FinalDate");
            DropColumn("dbo.Promotions", "InitialDate");
        }
    }
}
