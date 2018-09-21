using FifaBattle.Core;
using FifaBattle.Core.Dtos;
using FifaBattle.Core.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace FifaBattle.Controllers.Api
{
	public class MatchesController : ApiController
	{
		private IUnitOfWork _unitOfWork;

		public MatchesController(IUnitOfWork unitOfwork)
		{
			_unitOfWork = unitOfwork;
		}

		// GET: api/Matches
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET: api/Matches/5
		public string Get(int id)
		{
			return "value";
		}

		// POST: api/Matches
		public void Post([FromBody]string value)
		{

		}

		// PUT: api/Matches/5
		[Route("api/matches/{id}/{tournamentId}")]
		public IHttpActionResult Put(int id, string tournamentId, MatchDto dto)
		{
			//var userId = User.Identity.GetUserId();
			var userId = "3f310a65-509d-43a2-8714-c7626992c3d8";

			object[] keys = new object[2] { id, tournamentId };

			Match matchInDb = _unitOfWork.Matches.Get(keys);

			if (matchInDb == null)
				return NotFound();

			var tournamentInDb = _unitOfWork.Tournaments.Get(matchInDb.TournamentId);

			if (tournamentInDb.CreatorId != userId)
				return Unauthorized();

			if (dto.TeamType == "Home")
				matchInDb.HomeTeamGoals = dto.Goals;
			else
				matchInDb.AwayTeamGoals = dto.Goals;

			_unitOfWork.Commit();

			return Ok();
		}

		// DELETE: api/Matches/5
		public void Delete(int id)
		{
		}
	}
}
