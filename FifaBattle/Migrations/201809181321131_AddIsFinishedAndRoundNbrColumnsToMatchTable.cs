namespace FifaBattle.Migrations
{
	using System.Data.Entity.Migrations;

	public partial class AddIsFinishedAndRoundNbrColumnsToMatchTable : DbMigration
	{
		public override void Up()
		{
			AddColumn("dbo.Matches", "IsFinished", c => c.Boolean(nullable: false, defaultValue: false));
			AddColumn("dbo.Matches", "RoundNbr", c => c.Short(nullable: false, defaultValue: 1));
		}

		public override void Down()
		{
			DropColumn("dbo.Matches", "RoundNbr");
			DropColumn("dbo.Matches", "IsFinished");
		}
	}
}
