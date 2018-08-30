namespace FifaBattle.Migrations
{
	using System.Data.Entity.Migrations;

	public partial class AddCreatorIdForTournamentTable : DbMigration
	{
		public override void Up()
		{
			Sql("DELETE FROM dbo.Tournaments");

			AddColumn("dbo.Tournaments", "CreatorId", c => c.String(nullable: false, maxLength: 128));
			CreateIndex("dbo.Tournaments", "CreatorId");
			AddForeignKey("dbo.Tournaments", "CreatorId", "dbo.AspNetUsers", "Id", cascadeDelete: false);
		}
		
		public override void Down()
		{
			DropForeignKey("dbo.Tournaments", "CreatorId", "dbo.AspNetUsers");
			DropIndex("dbo.Tournaments", new[] { "CreatorId" });
			DropColumn("dbo.Tournaments", "CreatorId");
		}
	}
}
