using System.Collections.Generic;

namespace FifaBattle.Core.Repository
{
	public interface IRepository<T> where T : class
	{
		T Get(object key);

		IEnumerable<T> GetAll();

		void Add(T entity);

		void Remove(T entity);
	}
}
