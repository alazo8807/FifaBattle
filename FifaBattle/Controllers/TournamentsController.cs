using FifaBattle.Core;
using FifaBattle.Core.Models;
using FifaBattle.Core.ViewModels;
using FifaBattle.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace FifaBattle.Controllers
{
	public class TournamentsController : Controller
	{
		private IUnitOfWork _unitWork;
		private ApplicationDbContext _context;

		public TournamentsController(IUnitOfWork unitOfWork)
		{
			_unitWork = unitOfWork;
			_context = new ApplicationDbContext();
		}

		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		// GET: Tournaments
		public ActionResult Index()
		{
			var tournaments = _unitWork.Tournaments.GetTournamentsWithTournamentType();
			return View(tournaments);
		}

		//Tournaments/New
		public ActionResult New()
		{
			var viewModel = new TournamentViewModel
			{
				Title = "Create Tournament",
				TournamentTypes = _context.TournamentType.ToList(),
				Players = new List<Player>
				{
					new Player (),
					new Player ()
				}
			};

			return View("TournamentForm", viewModel);
		}

		[HttpPost]
		public ActionResult Create(TournamentViewModel tournamentVM)
		{
			if (!ModelState.IsValid)
			{
				tournamentVM.Title = "Create Tournament";
				tournamentVM.TournamentTypes = _context.TournamentType.ToList();
				return View("TournamentForm", tournamentVM);
			}

			var tournament = new Tournament
			{
				Id = Guid.NewGuid().ToString(),
				Players = new List<Player>(),
				Name = tournamentVM.Name,
				NumberOfPlayers = tournamentVM.NumberOfPlayers,
				TournamentTypeId = tournamentVM.TournamentTypeId,
				CreatorId = "3f310a65-509d-43a2-8714-c7626992c3d8",//User.Identity.GetUserId(),
				DateCreated = DateTime.Now
			};

			_context.Tournaments.Add(tournament);

			foreach (var player in tournamentVM.Players)
			{
				player.Id = Guid.NewGuid().ToString();
				player.TournamentId = tournament.Id;
				_context.Players.Add(player);
			}

			_context.SaveChanges();

			//MatchesGenerator matchesGenerator = new MatchesGenerator(tournament.Id);
			//matchesGenerator.Generate();

			return RedirectToAction("Index");
		}

		//Tournaments/Edit/Id
		public ActionResult Edit(string Id)
		{
			//var userId = User.Identity.GetUserId();
			var userId = "3f310a65-509d-43a2-8714-c7626992c3d8";

			//var tournamentInDb = _context.Tournaments.Single(t => t.Id == Id && t.CreatorId == userId);
			var tournamentInDb = _unitWork.Tournaments.GetTournamentWithAll(Id);

			if (tournamentInDb == null)
				return HttpNotFound();

			if (tournamentInDb.CreatorId != userId)
				return new HttpUnauthorizedResult();

			//var playersInDb = _context.Players.Where(p => p.TournamentId == tournamentInDb.Id).Include(c => c.Team).ToList();

			var viewModel = new TournamentViewModel
			{
				Title = "Edit Tournament",
				Id = tournamentInDb.Id,
				Name = tournamentInDb.Name,
				NumberOfPlayers = tournamentInDb.NumberOfPlayers,
				TournamentTypeId = tournamentInDb.TournamentTypeId,
				TournamentTypes = _unitWork.TournamentTypes.GetAll(),
				Players = tournamentInDb.Players
			};

			return View("TournamentForm", viewModel);
		}

		[HttpPost]
		public ActionResult Update(TournamentViewModel tournamentViewModel)
		{
			if (!ModelState.IsValid)
			{
				tournamentViewModel.Title = "Create Tournament";
				tournamentViewModel.TournamentTypes = _unitWork.TournamentTypes.GetAll();
				return View("TournamentForm", tournamentViewModel);
			}

			//var userId = User.Identity.GetUserId();
			var userId = "3f310a65-509d-43a2-8714-c7626992c3d8";

			var tournament = _unitWork.Tournaments.GetTournamentWithAll(tournamentViewModel.Id);

			if (tournament.Id == null)
				return HttpNotFound();

			if (tournament.CreatorId != userId)
				return new HttpUnauthorizedResult();

			tournament.Name = tournamentViewModel.Name;
			tournament.NumberOfPlayers = tournamentViewModel.NumberOfPlayers;
			tournament.TournamentTypeId = tournamentViewModel.TournamentTypeId;

			_context.Entry(tournament).State = System.Data.Entity.EntityState.Modified;

			foreach (var player in tournamentViewModel.Players)
			{
				if (player.Id == null || player.Id == "0")
				{
					player.Id = Guid.NewGuid().ToString();
					player.TournamentId = tournament.Id;
					_context.Players.Add(player);
				}
				else
				{
					var playerInDb = _context.Players.Include(p => p.Team).Single(p => p.Id == player.Id);
					playerInDb.Name = player.Name;
					playerInDb.Team.Name = player.Team.Name;
				}
			}

			var result = _context.SaveChanges();

			return RedirectToAction("Index");
		}
		//Tournaments/Delete/Id
		public ActionResult Delete(string id)
		{
			//var userId = User.Identity.GetUserId();
			var userId = "3f310a65-509d-43a2-8714-c7626992c3d8";

			//var tournamentInDb = _context.Tournaments.Single(t => t.Id == id && t.CreatorId == userId);
			var tournamentInDb = _unitWork.Tournaments.Get(id);

			if (tournamentInDb == null)
				return HttpNotFound();

			if (tournamentInDb.CreatorId != userId)
				return new HttpUnauthorizedResult();

			_context.Tournaments.Remove(tournamentInDb);
			_context.SaveChanges();

			//var playersInDb = _context.Players.Include(p => p.Team).Where(p => p.TournamentId == tournamentInDb.Id).ToList();

			//foreach (var player in playersInDb)
			//{
			//	var teamInDb = _context.Teams.Find(player.Team.Id);

			//	if (teamInDb != null)
			//	{
			//		_context.Teams.Remove(teamInDb);
			//	}
			//	_context.SaveChanges();
			//}

			_context.Tournaments.Remove(tournamentInDb);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}
	}
}