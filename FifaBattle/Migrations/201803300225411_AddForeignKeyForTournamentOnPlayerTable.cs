namespace FifaBattle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeyForTournamentOnPlayerTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Players", "Tournament_Id", "dbo.Tournaments");
            DropIndex("dbo.Players", new[] { "Tournament_Id" });
            RenameColumn(table: "dbo.Players", name: "Tournament_Id", newName: "TournamentId");
            AlterColumn("dbo.Players", "TournamentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Players", "TournamentId");
            AddForeignKey("dbo.Players", "TournamentId", "dbo.Tournaments", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Players", "TournamentId", "dbo.Tournaments");
            DropIndex("dbo.Players", new[] { "TournamentId" });
            AlterColumn("dbo.Players", "TournamentId", c => c.Int());
            RenameColumn(table: "dbo.Players", name: "TournamentId", newName: "Tournament_Id");
            CreateIndex("dbo.Players", "Tournament_Id");
            AddForeignKey("dbo.Players", "Tournament_Id", "dbo.Tournaments", "Id");
        }
    }
}
