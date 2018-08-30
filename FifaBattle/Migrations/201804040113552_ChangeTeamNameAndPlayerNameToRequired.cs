namespace FifaBattle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTeamNameAndPlayerNameToRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Teams", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Players", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Players", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.Teams", "Name", c => c.String(maxLength: 50));
        }
    }
}
