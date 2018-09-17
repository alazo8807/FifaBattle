using FifaBattle.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace FifaBattle.Controllers.Api
{
	public class PlayersController : ApiController
	{
		private ApplicationDbContext _context;

		public PlayersController()
		{
			_context = new ApplicationDbContext();
		}

		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		// GET api/<controller>
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/<controller>/5
		public string Get(int id)
		{
			return "value";
		}

		// POST api/<controller>
		public void Post([FromBody]string value)
		{
		}

		// PUT api/<controller>/5
		public void Put(int id, [FromBody]string value)
		{
		}

		//// DELETE api/<controller>/5
		//public IHttpActionResult Delete(string id)
		//{
		//	var playerInDb = _context.Players.Include(p => p.Team).SingleOrDefault(c => c.Id == id);

		//	if (playerInDb == null)
		//		return NotFound();

		//	//TODO: Remove Teams for this player.

		//	_context.Players.Remove(playerInDb);
		//	_context.SaveChanges();

		//	return Ok();
		//}
	}
}