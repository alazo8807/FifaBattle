using FifaBattle.Core;
using FifaBattle.Models;
using FifaBattle.Persistance;
using System.Web.Http;

namespace FifaBattle.Controllers.Api
{
	public class TournamentsController : ApiController
	{
		private ApplicationDbContext _context;
		private IUnitOfWork _unitOfWork;

		public TournamentsController()
		{
			_context = new ApplicationDbContext();
			_unitOfWork = new UnitOfWork(_context);
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

			_unitOfWork.Tournaments.Remove(tournamentInDb);
			_unitOfWork.Commit();

			return Ok();
		}
	}
}