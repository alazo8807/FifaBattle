using FifaBattle.Core.Domain;
using FifaBattle.Models;
using FifaBattle.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace FifaBattle.Controllers
{
	public class TournamentsController : Controller
	{
		private ApplicationDbContext _context;

		public TournamentsController()
		{
			_context = new ApplicationDbContext();
		}

		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		// GET: Tournaments
		public ActionResult Index()
		{
			var tournaments = _context.Tournaments.Include(t => t.TournamentType).ToList();
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
					new Player { Team = new Team() },
					new Player { Team = new Team() }
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
				Players = new Collection<Player>(),
				Name = tournamentVM.Name,
				NumberOfPlayers = tournamentVM.NumberOfPlayers,
				TournamentTypeId = tournamentVM.TournamentTypeId,
				CreatorId = User.Identity.GetUserId(),
				DateCreated = DateTime.Now
			};

			_context.Tournaments.Add(tournament);
			_context.SaveChanges();

			foreach (var player in tournamentVM.Players)
			{
				player.Id = Guid.NewGuid().ToString();
			}

			tournament.AddPlayers(tournamentVM.Players);
			_context.SaveChanges();

			//MatchesGenerator matchesGenerator = new MatchesGenerator(tournament.Id);
			//matchesGenerator.Generate();

			return RedirectToAction("Index");
		}

		//Tournaments/Edit/Id
		public ActionResult Edit(string Id)
		{
			var userId = User.Identity.GetUserId();

			var tournamentInDb = _context.Tournaments.Single(t => t.Id == Id && t.CreatorId == userId);

			if (tournamentInDb == null)
				return HttpNotFound();

			var playersInDb = _context.Players.Where(p => p.TournamentId == tournamentInDb.Id).Include(c => c.Team).ToList();

			var viewModel = new TournamentViewModel
			{
				Title = "Edit Tournament",
				Id = tournamentInDb.Id,
				Name = tournamentInDb.Name,
				NumberOfPlayers = tournamentInDb.NumberOfPlayers,
				TournamentTypeId = tournamentInDb.TournamentTypeId,
				TournamentTypes = _context.TournamentType.ToList(),
				Players = playersInDb
			};

			return View("TournamentForm", viewModel);
		}

		[HttpPost]
		public ActionResult Update(TournamentViewModel TournamentViewModel)
		{
			if (!ModelState.IsValid)
			{
				TournamentViewModel.Title = "Create Tournament";
				TournamentViewModel.TournamentTypes = _context.TournamentType.ToList();
				return View("TournamentForm", TournamentViewModel);
			}

			var userId = User.Identity.GetUserId();

			var players = TournamentViewModel.Players;

			var tournament = _context.Tournaments.Single(t => t.Id == TournamentViewModel.Id && t.CreatorId == userId);

			tournament.Name = TournamentViewModel.Name;
			tournament.NumberOfPlayers = TournamentViewModel.NumberOfPlayers;
			tournament.TournamentTypeId = TournamentViewModel.TournamentTypeId;

			_context.SaveChanges();

			foreach (var player in players)
			{
				if (player.Id != null)
				{
					player.TournamentId = tournament.Id;

					_context.Players.Add(player);
				}
				else
				{
					var playerInDb = _context.Players.Find(player.Id);
					playerInDb.Name = player.Name;

					var teamInDb = _context.Teams.Find(player.Team.Id);
					teamInDb.Name = player.Team.Name;

					playerInDb.TeamId = teamInDb.Id;
				}
			}

			_context.SaveChanges();

			return RedirectToAction("Index");
		}
		//Tournaments/Delete/Id
		public ActionResult Delete(string id)
		{
			var userId = User.Identity.GetUserId();

			var tournamentInDb = _context.Tournaments.Single(t => t.Id == id && t.CreatorId == userId);

			if (tournamentInDb == null)
				return HttpNotFound();

			var playersInDb = _context.Players.Include(p => p.Team).Where(p => p.TournamentId == tournamentInDb.Id).ToList();

			foreach (var player in playersInDb)
			{
				var teamInDb = _context.Teams.Find(player.Team.Id);

				if (teamInDb != null)
				{
					_context.Teams.Remove(teamInDb);
				}
				_context.SaveChanges();
			}
			_context.Tournaments.Remove(tournamentInDb);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}
	}
}