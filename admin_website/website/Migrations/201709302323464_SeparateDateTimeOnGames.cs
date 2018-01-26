namespace website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeparateDateTimeOnGames : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "Time", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "Time");
        }
    }
}
