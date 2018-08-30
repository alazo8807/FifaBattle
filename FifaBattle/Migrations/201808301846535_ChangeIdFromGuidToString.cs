namespace FifaBattle.Migrations
{
	using System.Data.Entity.Migrations;

	public partial class ChangeIdFromGuidToString : DbMigration
    {
		public override void Up()
		{
			DropForeignKey("dbo.Players", "TournamentId", "dbo.Tournaments");
			DropForeignKey("dbo.Matches", "TournamentId", "dbo.Tournaments");
			DropIndex("dbo.Matches", new[] { "TournamentId" });
			DropIndex("dbo.Players", new[] { "TournamentId" });
			DropPrimaryKey("dbo.Matches");
			DropPrimaryKey("dbo.Tournaments");
			DropPrimaryKey("dbo.Players");

			AlterColumn("dbo.Matches", "TournamentId", c => c.String(nullable: false, maxLength: 128));
			AlterColumn("dbo.Tournaments", "Id", c => c.String(nullable: false, maxLength: 128));
			AlterColumn("dbo.Players", "Id", c => c.String(nullable: false, maxLength: 128));
			AlterColumn("dbo.Players", "TournamentId", c => c.String(maxLength: 128));

			AddPrimaryKey("dbo.Matches", new[] { "Id", "TournamentId" });
			AddPrimaryKey("dbo.Tournaments", "Id");
			AddPrimaryKey("dbo.Players", "Id");
			CreateIndex("dbo.Matches", "TournamentId");
			CreateIndex("dbo.Players", "TournamentId");
			AddForeignKey("dbo.Players", "TournamentId", "dbo.Tournaments", "Id");
			AddForeignKey("dbo.Matches", "TournamentId", "dbo.Tournaments", "Id", cascadeDelete: true);
		}

		public override void Down()
		{
			DropForeignKey("dbo.Matches", "TournamentId", "dbo.Tournaments");
			DropForeignKey("dbo.Players", "TournamentId", "dbo.Tournaments");
			DropIndex("dbo.Players", new[] { "TournamentId" });
			DropIndex("dbo.Matches", new[] { "TournamentId" });
			DropPrimaryKey("dbo.Players");
			DropPrimaryKey("dbo.Tournaments");
			DropPrimaryKey("dbo.Matches");
			AlterColumn("dbo.Tournaments", "Id", c => c.Guid(nullable: false, identity: true));
			AlterColumn("dbo.Players", "Id", c => c.Guid(nullable: false, identity: true));
			AlterColumn("dbo.Players", "TournamentId", c => c.Guid(nullable: false, identity: false));
			AlterColumn("dbo.Matches", "TournamentId", c => c.Guid(nullable: false, identity: false));
			AddPrimaryKey("dbo.Players", "Id");
			AddPrimaryKey("dbo.Tournaments", "Id");
			AddPrimaryKey("dbo.Matches", new[] { "Id", "TournamentId" });
			CreateIndex("dbo.Players", "TournamentId");
			CreateIndex("dbo.Matches", "TournamentId");
			AddForeignKey("dbo.Matches", "TournamentId", "dbo.Tournaments", "Id", cascadeDelete: true);
			AddForeignKey("dbo.Players", "TournamentId", "dbo.Tournaments", "Id", cascadeDelete: true);
		}
	}
}
