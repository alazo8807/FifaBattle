namespace FifaBattle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTournamentTypeIdForeignKeyToTournamentTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tournaments", "TournamentTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tournaments", "TournamentTypeId");
            
        }
        
        public override void Down()
        {
            
            DropIndex("dbo.Tournaments", new[] { "TournamentTypeId" });
            DropColumn("dbo.Tournaments", "TournamentTypeId");
        }
    }
}
