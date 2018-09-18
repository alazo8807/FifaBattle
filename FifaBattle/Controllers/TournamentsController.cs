using FifaBattle.Core;
using FifaBattle.Core.Models;
using FifaBattle.Core.ViewModels;
using FifaBattle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FifaBattle.Controllers
{
	public class TournamentsController : Controller
	{
		private IUnitOfWork _unitOfWork;
		private ApplicationDbContext _context;

		public TournamentsController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_context = new ApplicationDbContext();
		}

		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		// GET: Tournaments
		public ActionResult Index()
		{
			var tournaments = _unitOfWork.Tournaments.GetTournamentsWithTournamentType();
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

			_unitOfWork.Tournaments.Add(tournament);

			foreach (var player in tournamentVM.Players)
			{
				player.Id = Guid.NewGuid().ToString();
				player.TournamentId = tournament.Id;
				_unitOfWork.Players.Add(player);
			}

			_unitOfWork.Commit();

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
			var tournamentInDb = _unitOfWork.Tournaments.GetTournamentWithAll(Id);

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
				TournamentTypes = _unitOfWork.TournamentTypes.GetAll(),
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
				tournamentViewModel.TournamentTypes = _unitOfWork.TournamentTypes.GetAll();
				return View("TournamentForm", tournamentViewModel);
			}

			//var userId = User.Identity.GetUserId();
			var userId = "3f310a65-509d-43a2-8714-c7626992c3d8";

			var tournament = _unitOfWork.Tournaments.GetTournamentWithAll(tournamentViewModel.Id);

			if (tournament.Id == null)
				return HttpNotFound();

			if (tournament.CreatorId != userId)
				return new HttpUnauthorizedResult();

			tournament.Name = tournamentViewModel.Name;
			tournament.NumberOfPlayers = tournamentViewModel.NumberOfPlayers;
			tournament.TournamentTypeId = tournamentViewModel.TournamentTypeId;

			//_context.Entry(tournament).State = System.Data.Entity.EntityState.Modified;

			foreach (var player in tournamentViewModel.Players)
			{
				if (player.Id == null || player.Id == "0")
				{
					player.Id = Guid.NewGuid().ToString();
					player.TournamentId = tournament.Id;
					_unitOfWork.Players.Add(player);
				}
				else
				{
					var playerInDb = _unitOfWork.Players.GetPlayerWithTeam(player.Id);
					playerInDb.Name = player.Name;
					playerInDb.Team.Name = player.Team.Name;
				}
			}

			_unitOfWork.Commit();

			return RedirectToAction("Index");
		}
	}
}