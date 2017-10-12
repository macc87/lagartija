namespace website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lineupsgamescontest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        SignedUp = c.Int(nullable: false),
                        MaxCapacity = c.Int(nullable: false),
                        EntryFee = c.Double(nullable: false),
                        Cap = c.Double(nullable: false),
                        StarTime = c.DateTime(nullable: false),
                        ContestType_Id = c.Int(nullable: false),
                        LineUp_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContestTypes", t => t.ContestType_Id, cascadeDelete: false)
                .ForeignKey("dbo.LineUps", t => t.LineUp_Id)
                .Index(t => t.ContestType_Id)
                .Index(t => t.LineUp_Id);
            
            CreateTable(
                "dbo.ContestTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Scheduled = c.DateTime(nullable: false),
                        Temperture = c.Double(nullable: false),
                        Humidity = c.Double(nullable: false),
                        AwayTeam_Id = c.Int(nullable: false),
                        HomeTeam_Id = c.Int(nullable: false),
                        Venue_Id = c.Int(nullable: false),
                        Wheather_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.AwayTeam_Id, cascadeDelete: false)
                .ForeignKey("dbo.Teams", t => t.HomeTeam_Id, cascadeDelete: false)
                .ForeignKey("dbo.Stadia", t => t.Venue_Id, cascadeDelete: false)
                .ForeignKey("dbo.ClimaConditions", t => t.Wheather_Id)
                .Index(t => t.AwayTeam_Id)
                .Index(t => t.HomeTeam_Id)
                .Index(t => t.Venue_Id)
                .Index(t => t.Wheather_Id);
            
            CreateTable(
                "dbo.LineUps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LineUpToContests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContestId = c.Int(nullable: false),
                        LineUpId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contests", t => t.ContestId, cascadeDelete: false)
                .ForeignKey("dbo.LineUps", t => t.LineUpId, cascadeDelete: false)
                .Index(t => t.ContestId)
                .Index(t => t.LineUpId);
            
            CreateTable(
                "dbo.LineUpToPlayers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlayerId = c.Int(nullable: false),
                        LineUpId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LineUps", t => t.LineUpId, cascadeDelete: false)
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: false)
                .Index(t => t.PlayerId)
                .Index(t => t.LineUpId);
            
            AddColumn("dbo.Players", "LineUp_Id", c => c.Int());
            CreateIndex("dbo.Players", "LineUp_Id");
            AddForeignKey("dbo.Players", "LineUp_Id", "dbo.LineUps", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LineUpToPlayers", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.LineUpToPlayers", "LineUpId", "dbo.LineUps");
            DropForeignKey("dbo.LineUpToContests", "LineUpId", "dbo.LineUps");
            DropForeignKey("dbo.LineUpToContests", "ContestId", "dbo.Contests");
            DropForeignKey("dbo.Players", "LineUp_Id", "dbo.LineUps");
            DropForeignKey("dbo.Contests", "LineUp_Id", "dbo.LineUps");
            DropForeignKey("dbo.Games", "Wheather_Id", "dbo.ClimaConditions");
            DropForeignKey("dbo.Games", "Venue_Id", "dbo.Stadia");
            DropForeignKey("dbo.Games", "HomeTeam_Id", "dbo.Teams");
            DropForeignKey("dbo.Games", "AwayTeam_Id", "dbo.Teams");
            DropForeignKey("dbo.Contests", "ContestType_Id", "dbo.ContestTypes");
            DropIndex("dbo.LineUpToPlayers", new[] { "LineUpId" });
            DropIndex("dbo.LineUpToPlayers", new[] { "PlayerId" });
            DropIndex("dbo.LineUpToContests", new[] { "LineUpId" });
            DropIndex("dbo.LineUpToContests", new[] { "ContestId" });
            DropIndex("dbo.Players", new[] { "LineUp_Id" });
            DropIndex("dbo.Games", new[] { "Wheather_Id" });
            DropIndex("dbo.Games", new[] { "Venue_Id" });
            DropIndex("dbo.Games", new[] { "HomeTeam_Id" });
            DropIndex("dbo.Games", new[] { "AwayTeam_Id" });
            DropIndex("dbo.Contests", new[] { "LineUp_Id" });
            DropIndex("dbo.Contests", new[] { "ContestType_Id" });
            DropColumn("dbo.Players", "LineUp_Id");
            DropTable("dbo.LineUpToPlayers");
            DropTable("dbo.LineUpToContests");
            DropTable("dbo.LineUps");
            DropTable("dbo.Games");
            DropTable("dbo.ContestTypes");
            DropTable("dbo.Contests");
        }
    }
}
