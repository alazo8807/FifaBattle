using FifaBattle.Core.Models;
using System;

namespace FifaBattle.Core.Helpers
{
	public class MatchesGenerator
	{
		private IUnitOfWork _unitOfWork;

		private Tournament _tournament;

		public MatchesGenerator(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_tournament = new Tournament();
		}

		//Adding a comment agains
		public void GenerateMatches(string tournamentId)
		{
			if (string.IsNullOrEmpty(tournamentId))
				throw new ArgumentException("tournamentId parameter cannot be null or empty");

			var tournamentInDb = _unitOfWork.Tournaments.GetTournamentWithAll(tournamentId);

			if (tournamentInDb == null)
				throw new Exception("Tournament not found");

			_tournament = tournamentInDb;

			switch (tournamentInDb.TournamentType.Name)
			{
				case "League":
					GenerateLeague();
					break;
				default:
					break;
			}
		}

		private void GenerateLeague(bool twoRoundsLeague = false)
		{
			int roundNo = 1;
			int prevRoundNo = 1;
			var playersCount = _tournament.Players.Count;

			if (playersCount == 1)
				return;

			if (playersCount == 2)
			{
				AddMatch(_tournament.Players[0].TeamId, _tournament.Players[1].TeamId, roundNo);
				return;
			}

			for (int i = 0; i < _tournament.Players.Count; i++)
			{
				if (i > 0)
				{
					roundNo = prevRoundNo + 2;

					if ((roundNo - playersCount) > 0)
					{
						roundNo -= playersCount;
						prevRoundNo = roundNo;
					}
				}

				for (int j = i + 1; j < _tournament.Players.Count; j++)
				{
					AddMatch(_tournament.Players[i].TeamId, _tournament.Players[j].TeamId, 1);

					roundNo += 1;

					if (roundNo > playersCount)
					{
						roundNo = 1;
					}
				}
			}
		}


		private void AddMatch(int homeTeamId, int awayTeamId, int roundNo)
		{
			var match = new Match
			{
				TournamentId = _tournament.Id,
				HomeTeamId = homeTeamId,
				AwayTeamId = awayTeamId,
				RoundNbr = (short)roundNo
			};

			_unitOfWork.Matches.Add(match);
			_unitOfWork.Commit();
		}
	}
}