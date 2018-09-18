using FifaBattle.Core.Models;
using FifaBattle.Core.Repository;

namespace FifaBattle.Core
{
	public interface IUnitOfWork
	{
		IRepository<Team> Teams { get; set; }

		void Dispose();

		int Commit();

		ITournamentsRepository Tournaments { get; set; }
		IPlayersRepository Players { get; set; }
		IRepository<TournamentType> TournamentTypes { get; set; }
	}
}
