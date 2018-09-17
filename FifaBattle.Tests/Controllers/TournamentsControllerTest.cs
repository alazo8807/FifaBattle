using FifaBattle.Controllers;
using FifaBattle.Core;
using FifaBattle.Core.Models;
using FifaBattle.Core.Repository;
using FifaBattle.Core.ViewModels;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FifaBattle.Tests.Controllers
{
	/// <summary>
	/// Summary description for TournamentsControllerTest
	/// </summary>
	[TestFixture]
	public class TournamentsControllerTest
	{
		private TournamentsController _controller;
		private Mock<ITournamentsRepository> _tournamentRepo;
		private Mock<IUnitOfWork> _uow;


		public TournamentsControllerTest()
		{
			_tournamentRepo = new Mock<ITournamentsRepository>();
			_uow = new Mock<IUnitOfWork>();
		}

		[SetUp]
		public void Init()
		{
			_uow.SetupGet(u => u.Tournaments).Returns(_tournamentRepo.Object);
			_controller = new TournamentsController(_uow.Object);

		}

		[TearDown]
		public void Clean()
		{

		}

		[Test]
		public void Index_WhenCalled_ShouldReceiveTournamentsList()
		{
			List<Tournament> tournamentStub = new List<Tournament>
			{
				new Tournament
				{
					Id = "1",
					Name = "Test Tournament",
					TournamentTypeId = 1,
					NumberOfPlayers = 2
				},
				new Tournament
				{
					Id = "2",
					Name = "Test Tournament 2",
					TournamentTypeId = 1,
					NumberOfPlayers = 2
				}
			};

			_tournamentRepo.Setup(t => t.GetAll()).Returns(tournamentStub);

			var tournamentVM = new List<TournamentViewModel>();
			foreach (var tournament in tournamentStub)
			{
				tournamentVM.Add(new TournamentViewModel
				{
					Id = tournament.Id,
					Name = tournament.Name,
					TournamentTypeId = tournament.TournamentTypeId,
					NumberOfPlayers = tournament.NumberOfPlayers
				});
			}

			var result = _controller.Index() as ViewResult;
			var model = result.Model as List<TournamentViewModel>;

			for (int i = 0; i < model.Count; i++)
			{
				Assert.That(model[i].Id, Is.EqualTo(tournamentVM[i].Id));
				Assert.That(model[i].Name, Is.EqualTo(tournamentVM[i].Name));
				Assert.That(model[i].TournamentTypeId, Is.EqualTo(tournamentVM[i].TournamentTypeId));
			}

		}
	}
}
