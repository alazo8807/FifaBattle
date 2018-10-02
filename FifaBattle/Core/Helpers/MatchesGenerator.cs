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
			var playersCount = _tournament.Players.Count;

			if (playersCount == 1)
				return;

			if (playersCount == 2)
			{
				AddMatch(_tournament.Players[0].TeamId, _tournament.Players[1].TeamId, 1);
				return;
			}

			int[,] matchGuideArray = createGuideArray(playersCount);

			for (int i = 0; i < playersCount; i++)
			{
				for (int j = i + 1; j < _tournament.Players.Count; j++)
				{
					AddMatch(_tournament.Players[i].TeamId, _tournament.Players[j].TeamId, matchGuideArray[i, j]);
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

		private int[,] createGuideArray(int size)
		{
			int[,] GuideArray = new int[size, size];

			var matchIndex = size;

			for (int i = 0; i < size; i++)
			{
				if (i > 0) matchIndex = GuideArray[i - 1, 0] + 1;

				if (matchIndex > size) matchIndex = 1;

				for (int j = 0; j < size; j++)
				{
					if (matchIndex > size)
						matchIndex = 1;

					GuideArray[i, j] = matchIndex;
					matchIndex++;
				}
			}

			return GuideArray;
		}
	}
}