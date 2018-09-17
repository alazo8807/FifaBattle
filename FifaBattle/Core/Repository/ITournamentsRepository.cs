using FifaBattle.Core.Models;
using System.Collections.Generic;

namespace FifaBattle.Core.Repository
{
	public interface ITournamentsRepository : IRepository<Tournament>
	{
		Tournament GetTournamentWithTournamentType(string id);

		Tournament GetTournamentWithAll(string id);

		IEnumerable<Tournament> GetTournamentsWithTournamentType();
	}
}
