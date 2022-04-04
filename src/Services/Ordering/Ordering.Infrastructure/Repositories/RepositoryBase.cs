using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Common;

namespace Ordering.Infrastructure.Repositories;

public abstract class RepositoryBase<TSource> : IAsyncRepository<TSource> where TSource : EntityBase
{
	private readonly DbContext _context;

	protected RepositoryBase(DbContext context)
	{
		_context = context;
	}

	public async Task<IReadOnlyList<TSource>> FindAll() => await _context.Set<TSource>().AsNoTracking().ToListAsync();

	public async Task<IReadOnlyList<TSource>> FindAsync(Expression<Func<TSource, bool>> predicate) =>
		await _context.Set<TSource>().Where(predicate).AsNoTracking().ToListAsync();

	public async Task<IReadOnlyList<TSource>> FindAsync(Expression<Func<TSource, bool>> predicate,
		Func<IQueryable<TSource>, IOrderedQueryable<TSource>>? orderBy = null, string? includes = null,
		bool trackChanges = false)
	{
		var query = (trackChanges) ? _context.Set<TSource>().AsTracking() : _context.Set<TSource>().AsNoTracking();
		if (!string.IsNullOrEmpty(includes)) query = query.Include(includes);
		if (predicate != null) query = query.Where(predicate);
		if (orderBy != null) return await orderBy(query).ToListAsync();
		return await query.ToListAsync();
	}

	public async Task<IReadOnlyList<TSource>> FindAsync(Expression<Func<TSource, bool>> predicate,
		Func<IQueryable<TSource>, IOrderedQueryable<TSource>>? orderBy = null,
		List<Expression<Func<TSource, object>>> includes = null, bool trackChanges = false)
	{
		var query = (trackChanges) ? _context.Set<TSource>().AsTracking() : _context.Set<TSource>().AsNoTracking();
		if (includes != null)
		{
			query = includes.Aggregate(query, (current, include) => current.Include(include));
		}

		if (predicate != null)
		{
			query = query.Where(predicate);
		}

		return await query.ToListAsync();
	}

	public async Task<TSource?> FindById(int id)
	{
		var sets = _context.Set<TSource>();
		return await sets.FirstOrDefaultAsync(e => e.Id == id);
	}

	public async Task<TSource> AddAsync(TSource entity)
	{
		_context.Set<TSource>().Add(entity);
		await _context.SaveChangesAsync();
		return entity;
	}

	public async Task UpdateAsync(TSource entity)
	{
		_context.Entry(entity).State = EntityState.Modified;
		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(TSource entity)
	{
		_context.Set<TSource>().Remove(entity);
		await _context.SaveChangesAsync();
	}
}