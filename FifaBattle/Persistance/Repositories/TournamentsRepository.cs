using FifaBattle.Core.Models;
using FifaBattle.Core.Repository;
using FifaBattle.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace FifaBattle.Persistance.Repositories
{
	public class TournamentsRepository : Repository<Tournament>, ITournamentsRepository
	{
		public IEnumerable<T> Find(Expression<Func<T, bool>> criteria)
		{
			throw new NotImplementedException();
		}
	}
}