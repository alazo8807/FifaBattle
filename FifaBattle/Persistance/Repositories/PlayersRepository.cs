using FifaBattle.Core.Models;
using FifaBattle.Core.Repository;
using FifaBattle.Models;
using System.Data.Entity;
using System.Linq;

namespace FifaBattle.Persistance.Repositories
{
	public class PlayersRepository : Repository<Player>, IPlayersRepository
	{
		public PlayersRepository(ApplicationDbContext context)
		: base(context)
		{
		}

		public Player GetPlayerWithTeam(string id)
		{
			return _context.Players.Include(p => p.Team).Single(t => t.Id == id);
		}
	}
}