using System.Collections.Generic;
using System.Linq.Expressions;

namespace FifaBattle.Core.Repository
{
	public interface IRepository<T> where T : class
	{
		T Get(object key);

		IEnumerable<T> GetAll();

		IEnumerable<T> Find(Expression<System.Func<T, bool>> criteria);

		void Add(T entity);

		void Remove(T entity);
	}
}
