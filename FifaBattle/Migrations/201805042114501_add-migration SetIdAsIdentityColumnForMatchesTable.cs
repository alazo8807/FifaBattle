namespace FifaBattle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmigrationSetIdAsIdentityColumnForMatchesTable : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Matches");
            AlterColumn("dbo.Matches", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Matches", new[] { "Id", "TournamentId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Matches");
            AlterColumn("dbo.Matches", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Matches", new[] { "Id", "TournamentId" });
        }
    }
}
