namespace website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotificationsDates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "InitialDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Notifications", "FinalDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "FinalDate");
            DropColumn("dbo.Notifications", "InitialDate");
        }
    }
}
