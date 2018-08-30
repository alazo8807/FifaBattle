namespace FifaBattle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequiredAttributeToTournamentName : DbMigration
    {
        public override void Up()
        {
			Sql("UPDATE dbo.Tournaments SET Name = 'Default Tournament' WHERE name = '' or name is null");
            AlterColumn("dbo.Tournaments", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tournaments", "Name", c => c.String());
        }
    }
}
