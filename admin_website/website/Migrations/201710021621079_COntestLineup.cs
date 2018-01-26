namespace website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class COntestLineup : DbMigration
    {
        public override void Up()
        {
            /*CreateTable(
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
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContestTypes", t => t.ContestType_Id, cascadeDelete: false)
                .Index(t => t.ContestType_Id);

           CreateTable(
                "dbo.LineUps",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                })
                .PrimaryKey(t => t.Id);
                */
        }
        
        public override void Down()
        {
        }
    }
}
