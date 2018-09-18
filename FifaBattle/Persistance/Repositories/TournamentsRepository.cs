using FifaBattle.Core.Models;
using FifaBattle.Core.Repository;
using FifaBattle.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FifaBattle.Persistance.Repositories
{
	public class TournamentsRepository : Repository<Tournament>, ITournamentsRepository
	{
		public TournamentsRepository(ApplicationDbContext context)
			: base(context)
		{
		}

		public Tournament GetTournamentWithTournamentType(string id)
		{
			return _context.Tournaments.Include(t => t.TournamentType).Single(t => t.Id == id);
		}

		public Tournament GetTournamentWithAll(string id)
		{
			return _context.Tournaments
				.Include(t => t.TournamentType)
				.Include(t => t.Players)
				.Include(t => t.Players.Select(p => p.Team))
				.Single(t => t.Id == id);
		}

		public IEnumerable<Tournament> GetTournamentsWithTournamentType()
		{
			return _context.Tournaments.Include(t => t.TournamentType).ToList();
		}
	}
}