using FifaBattle.Core.Models;

namespace FifaBattle.Core.Repository
{
	public interface IPlayersRepository : IRepository<Player>
	{
		Player GetPlayerWithTeam(string id);
	}
}