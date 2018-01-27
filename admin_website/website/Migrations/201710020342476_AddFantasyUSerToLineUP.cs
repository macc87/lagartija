namespace website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFantasyUSerToLineUP : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LineUps", "User_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.LineUps", "User_Id");
            AddForeignKey("dbo.LineUps", "User_Id", "dbo.AspNetUsers", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LineUps", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.LineUps", new[] { "User_Id" });
            DropColumn("dbo.LineUps", "User_Id");
        }
    }
}
