using FifaBattle.Core.Domain;
using FifaBattle.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FifaBattle.Services
{
	public class MatchesGenerator
	{
		private ApplicationDbContext _context;

		private string _tournamentId;

		private IList<Player> _players;

		protected MatchesGenerator()
		{
		}

		public MatchesGenerator(string TournamentId)
		{
			_context = new ApplicationDbContext();

			//_tournamentId = TournamentId;

			_players = _context.Players
				.Include(p => p.Team)
				.Where(p => p.TournamentId == TournamentId)
				.ToList();
		}

		public void Generate()
		{
			var playersCount = _players.Count;

			for (int i = 0; i < playersCount; i++)
			{
				for (int j = i + 1; j < playersCount; j++)
				{
					var homeTeamId = _players[i].Team.Id;

					var awayTeamId = _players[j].Team.Id;

					CreateMatch(homeTeamId, awayTeamId);
				}
			}
		}

		//Adding a comment

		public void CreateMatch(int homeTeamId, int awayTeamId)
		{
			var match = new Match
			{
				TournamentId = _tournamentId,
				HomeTeamId = homeTeamId,
				AwayTeamId = awayTeamId
			};

			_context.Matches.Add(match);
			_context.SaveChanges();
		}

	}
}