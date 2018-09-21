﻿using FifaBattle.Core;
using FifaBattle.Models;
using System.Linq;
using System.Web.Http;

namespace FifaBattle.Controllers.Api
{
	public class TournamentsController : ApiController
	{
		private ApplicationDbContext _context;
		private readonly IUnitOfWork _unitOfWork;

		public TournamentsController(IUnitOfWork unitOfWork)
		{
			_context = new ApplicationDbContext();
			_unitOfWork = unitOfWork;
		}

		// DELETE api/<controller>/5
		[HttpDelete]
		public IHttpActionResult DeleteTournament(string id)
		{
			//var userId = User.Identity.GetUserId();
			var userId = "3f310a65-509d-43a2-8714-c7626992c3d8";

			var tournamentInDb = _unitOfWork.Tournaments.GetTournamentWithAll(id);

			if (tournamentInDb == null)
				return NotFound();

			if (tournamentInDb.CreatorId != userId)
				return Unauthorized();

			var matchesInDb = _unitOfWork.Matches.Find(m => m.TournamentId == id);

			if (matchesInDb != null)
			{
				foreach (var match in matchesInDb)
				{
					_unitOfWork.Matches.Remove(match);
				}
			}

			foreach (var player in tournamentInDb.Players.ToList())
			{
				_unitOfWork.Teams.Remove(player.Team);
				_unitOfWork.Players.Remove(player);
			}

			_unitOfWork.Tournaments.Remove(tournamentInDb);
			_unitOfWork.Commit();

			return Ok();
		}
	}
}