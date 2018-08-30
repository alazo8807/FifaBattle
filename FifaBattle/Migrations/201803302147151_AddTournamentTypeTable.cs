namespace FifaBattle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTournamentTypeTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TournamentTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);

			Sql("INSERT INTO dbo.TournamentTypes(Name) VALUES ('League')");
			Sql("INSERT INTO dbo.TournamentTypes(Name) VALUES ('Knockout')");
			Sql("INSERT INTO dbo.TournamentTypes(Name) VALUES ('League And Knockout')");
		}
        
        public override void Down()
        {
            DropTable("dbo.TournamentTypes");
        }
    }
}
