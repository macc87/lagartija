namespace website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MLBPlayerOnDelte : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MLBPlayers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Player_Id = c.Int(nullable: false),
                        Position_Id = c.Int(nullable: false),
                        Team_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.Player_Id, cascadeDelete: false)
                .ForeignKey("dbo.Positions", t => t.Position_Id, cascadeDelete: false)
                .ForeignKey("dbo.Teams", t => t.Team_Id, cascadeDelete: false)
                .Index(t => t.Player_Id)
                .Index(t => t.Position_Id)
                .Index(t => t.Team_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MLBPlayers", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.MLBPlayers", "Position_Id", "dbo.Positions");
            DropForeignKey("dbo.MLBPlayers", "Player_Id", "dbo.Players");
            DropIndex("dbo.MLBPlayers", new[] { "Team_Id" });
            DropIndex("dbo.MLBPlayers", new[] { "Position_Id" });
            DropIndex("dbo.MLBPlayers", new[] { "Player_Id" });
            DropTable("dbo.MLBPlayers");
        }
    }
}
