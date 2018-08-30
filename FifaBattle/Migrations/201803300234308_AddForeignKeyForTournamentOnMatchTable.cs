namespace FifaBattle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeyForTournamentOnMatchTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Matches", "Tournament_Id", "dbo.Tournaments");
            DropIndex("dbo.Matches", new[] { "Tournament_Id" });
            RenameColumn(table: "dbo.Matches", name: "Tournament_Id", newName: "TournamentId");
            AlterColumn("dbo.Matches", "TournamentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Matches", "TournamentId");
            AddForeignKey("dbo.Matches", "TournamentId", "dbo.Tournaments", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matches", "TournamentId", "dbo.Tournaments");
            DropIndex("dbo.Matches", new[] { "TournamentId" });
            AlterColumn("dbo.Matches", "TournamentId", c => c.Int());
            RenameColumn(table: "dbo.Matches", name: "TournamentId", newName: "Tournament_Id");
            CreateIndex("dbo.Matches", "Tournament_Id");
            AddForeignKey("dbo.Matches", "Tournament_Id", "dbo.Tournaments", "Id");
        }
    }
}
