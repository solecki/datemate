using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;

namespace DateMate.Data.Repositories
{
	//	Generic implementation of IRepository. Provides the standard peices needed for using
	//	a repository pattern.
	public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		// Should be used in derived repository classes.
		protected DbContext DataContext { get; }
		private DbSet<TEntity> _entities;

		public Repository(DbContext context)
		{
			DataContext = context;
			_entities = context.Set<TEntity>();
		}

		public TEntity GetById(int id1, int id2)
		{
			return _entities.Find(id1, id2);
		}

		public IEnumerable<TEntity> GetAll()
		{
			return _entities.ToList();
		}

		public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
		{
			return _entities.Where(predicate);
		}

		public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
		{
			return _entities.SingleOrDefault(predicate);
		}

		public void Add(TEntity entity)
		{
			_entities.Add(entity);
		}

		public void AddRange(IEnumerable<TEntity> entities)
		{
			_entities.AddRange(entities);
		}

		public void Remove(TEntity entity)
		{
			_entities.Remove(entity);
		}

		public void RemoveRange(IEnumerable<TEntity> entities)
		{
			_entities.RemoveRange(entities);
		}

		public void Edit(TEntity entity)
		{
			_entities.AddOrUpdate(entity);
			//DataContext.Entry(entity).State = EntityState.Modified; replaced with above line 'cause annoying bug.
		}
	}
}