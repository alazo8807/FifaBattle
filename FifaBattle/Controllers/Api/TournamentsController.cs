using FifaBattle.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace FifaBattle.Controllers.Api
{
	public class TournamentsController : ApiController
	{
		private ApplicationDbContext _context;

		public TournamentsController()
		{
			_context = new ApplicationDbContext();
		}

		// DELETE api/<controller>/5
		[HttpDelete]
		public IHttpActionResult DeleteTournament(string id)
		{
			var userId = User.Identity.GetUserId();

			var tournamentInDb = _context.Tournaments
				.Include(t => t.Players)
				.SingleOrDefault(c => c.Id == id && c.CreatorId == userId);

			if (tournamentInDb == null)
				throw new HttpResponseException(HttpStatusCode.NotFound);

			var teamIds = tournamentInDb.Players.Select(p => p.TeamId).ToList();

			_context.Tournaments.Remove(tournamentInDb);
			_context.SaveChanges();

			foreach (var teamId in teamIds)
			{
				var team = _context.Teams.SingleOrDefault(t => t.Id == teamId);

				if (team != null)
					_context.Teams.Remove(team);
			}

			_context.SaveChanges();

			return Ok();
		}
	}
}