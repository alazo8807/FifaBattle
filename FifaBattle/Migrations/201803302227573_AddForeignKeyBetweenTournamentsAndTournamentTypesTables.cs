namespace FifaBattle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeyBetweenTournamentsAndTournamentTypesTables : DbMigration
    {
        public override void Up()
        {
			Sql("UPDATE dbo.Tournaments SET TournamentTypeId=1");
			AddForeignKey("dbo.Tournaments", "TournamentTypeId", "dbo.TournamentTypes", "Id", cascadeDelete: false);
		}
        
        public override void Down()
        {
			DropForeignKey("dbo.Tournaments", "TournamentTypeId", "dbo.TournamentTypes");
		}
    }
}
