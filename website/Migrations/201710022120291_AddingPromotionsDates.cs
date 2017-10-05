namespace website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingPromotionsDates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Promotions", "InitialTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Promotions", "FinalTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Promotions", "FinalTime");
            DropColumn("dbo.Promotions", "InitialTime");
        }
    }
}
