using FifaBattle.Core.Models;
using FifaBattle.Core.Repository;

namespace FifaBattle.Core
{
	public interface IUnitOfWork
	{
		void Dispose();

		int Commit();

		ITournamentsRepository Tournaments { get; set; }
		IRepository<TournamentType> TournamentTypes { get; set; }
	}
}
