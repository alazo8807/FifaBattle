using FifaBattle.Core;
using FifaBattle.Core.Models;
using FifaBattle.Core.Repository;
using FifaBattle.Models;
using FifaBattle.Persistance.Repositories;

namespace FifaBattle.Persistance
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _context;

		public UnitOfWork(ApplicationDbContext context)
		{
			_context = new ApplicationDbContext();
			Tournaments = new TournamentsRepository(_context);
			Players = new PlayersRepository(_context);
			TournamentTypes = new Repository<TournamentType>(_context);
			Teams = new Repository<Team>(_context);
		}

		public ITournamentsRepository Tournaments { get; set; }
		public IPlayersRepository Players { get; set; }
		public IRepository<TournamentType> TournamentTypes { get; set; }
		public IRepository<Team> Teams { get; set; }

		public int Commit()
		{
			return _context.SaveChanges();
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}