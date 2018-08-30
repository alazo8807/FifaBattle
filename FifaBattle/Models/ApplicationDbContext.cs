using System.Data.Entity;
using FifaBattle.Core.Domain;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FifaBattle.Models
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public DbSet<Team> Teams { get; set; }
		public DbSet<Player> Players { get; set; }
		public DbSet<Match> Matches { get; set; }
		public DbSet<Tournament> Tournaments { get; set; }
		public DbSet<TournamentType> TournamentType { get; set; }

		public ApplicationDbContext()
			: base("FifaBattleContext", throwIfV1Schema: false)
		{
		}

		public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}
	}
}