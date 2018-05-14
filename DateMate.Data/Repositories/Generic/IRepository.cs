using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DateMate.Data.Repositories
{
	// Summary:
	//	Your normal repository interface.
	// Parameters:
	//	TEntity: The POCO type that the ORM system will operate on.
	public interface IRepository<TEntity> where TEntity : class
	{
		TEntity GetById(int id1, int id2); // For composite keys.
		IEnumerable<TEntity> GetAll();

		//	Search for objects in the repository using a lambda expression.
		IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

		void Add(TEntity entity);
		void AddRange(IEnumerable<TEntity> entities);

		void Remove(TEntity entity);
		void RemoveRange(IEnumerable<TEntity> entities);
		void Edit(TEntity entity);
	}
}
