using FifaBattle.Core.Repository;
using FifaBattle.Models;
using System.Collections.Generic;
using System.Linq;

namespace FifaBattle.Persistance.Repositories
{
	public class Repository<T> : IRepository<T> where T : class
	{
		protected ApplicationDbContext _context;

		public Repository(ApplicationDbContext context)
		{
			_context = context;
		}

		public T Get(object key)
		{
			return _context.Set<T>().Find(key);
		}

		public IEnumerable<T> GetAll()
		{
			return _context.Set<T>().ToList();
		}

		public void Add(T entity)
		{
			_context.Set<T>().Add(entity);
		}

		public void Remove(T entity)
		{
			_context.Set<T>().Remove(entity);
		}
	}
}