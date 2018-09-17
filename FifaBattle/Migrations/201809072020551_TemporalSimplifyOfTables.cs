namespace FifaBattle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TemporalSimplifyOfTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Players", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.Players", "TournamentId", "dbo.Tournaments");
            DropIndex("dbo.Players", new[] { "TeamId" });
            DropIndex("dbo.Players", new[] { "TournamentId" });
            DropColumn("dbo.Players", "TeamId");
            DropColumn("dbo.Players", "TournamentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Players", "TournamentId", c => c.String(maxLength: 128));
            AddColumn("dbo.Players", "TeamId", c => c.Int(nullable: false));
            CreateIndex("dbo.Players", "TournamentId");
            CreateIndex("dbo.Players", "TeamId");
            AddForeignKey("dbo.Players", "TournamentId", "dbo.Tournaments", "Id");
            AddForeignKey("dbo.Players", "TeamId", "dbo.Teams", "Id", cascadeDelete: true);
        }
    }
}
