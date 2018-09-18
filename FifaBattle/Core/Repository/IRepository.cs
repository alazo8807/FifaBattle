using System.Collections.Generic;
using System.Linq.Expressions;

namespace FifaBattle.Core.Repository
{
	public interface IRepository<T> where T : class
	{
		IEnumerable<T> Find(Expression<System.Func<T, bool>> criteria);
		T Get(object key);

		IEnumerable<T> GetAll();

		void Add(T entity);

		void Remove(T entity);
	}
}
