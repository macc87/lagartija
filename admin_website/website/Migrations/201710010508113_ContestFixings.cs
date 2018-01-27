namespace website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContestFixings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContestToGames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContestId = c.Int(nullable: false),
                        GameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contests", t => t.ContestId, cascadeDelete: false)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: false)
                .Index(t => t.ContestId)
                .Index(t => t.GameId);
            
            AddColumn("dbo.Games", "Contest_Id", c => c.Int());
            CreateIndex("dbo.Games", "Contest_Id");
            AddForeignKey("dbo.Games", "Contest_Id", "dbo.Contests", "Id");
            DropColumn("dbo.Contests", "StarTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contests", "StarTime", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.ContestToGames", "GameId", "dbo.Games");
            DropForeignKey("dbo.ContestToGames", "ContestId", "dbo.Contests");
            DropForeignKey("dbo.Games", "Contest_Id", "dbo.Contests");
            DropIndex("dbo.ContestToGames", new[] { "GameId" });
            DropIndex("dbo.ContestToGames", new[] { "ContestId" });
            DropIndex("dbo.Games", new[] { "Contest_Id" });
            DropColumn("dbo.Games", "Contest_Id");
            DropTable("dbo.ContestToGames");
        }
    }
}
