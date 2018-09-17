namespace FifaBattle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPlayersToTournaments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Players", "TournamentId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Players", "TournamentId");
            AddForeignKey("dbo.Players", "TournamentId", "dbo.Tournaments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Players", "TournamentId", "dbo.Tournaments");
            DropIndex("dbo.Players", new[] { "TournamentId" });
            DropColumn("dbo.Players", "TournamentId");
        }
    }
}
