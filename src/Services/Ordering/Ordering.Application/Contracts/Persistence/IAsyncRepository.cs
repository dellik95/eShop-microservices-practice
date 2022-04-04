using System.Linq.Expressions;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;

namespace Ordering.Application.Contracts.Persistence;

public interface IAsyncRepository<T> where T : EntityBase
{
	Task<IReadOnlyList<T>> FindAll();

	Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate);

	Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate,
		Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
		string? includes = null,
		bool trackChanges = false);

	Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate,
		Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
		List<Expression<Func<T, object>>> includes = null,
		bool trackChanges = false);

	Task<T> FindById(int id);
	Task<T> AddAsync(T entity);
	Task UpdateAsync(T entity);
	Task DeleteAsync(T entity);
}