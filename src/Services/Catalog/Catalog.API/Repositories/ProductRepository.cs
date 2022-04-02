using System.Linq.Expressions;
using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories;

public class ProductRepository : IProductRepository
{
	private readonly Expression<Func<Product, bool>> _findAllExpression = p => true;
	private readonly ICatalogContext _context;

	public ProductRepository(ICatalogContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<Product>> GetProductsByConditions(Expression<Func<Product, bool>>? expression)
	{
		var exp = expression ?? _findAllExpression;
		return await _context.Products.Find(exp).ToListAsync();
	}

	public async Task<Product> GetProductByConditions(Expression<Func<Product, bool>> expression)
	{
		return await _context.Products.Find(expression).FirstOrDefaultAsync();
	}

	public async Task CreateProduct(Product product)
	{
		await _context.Products.InsertOneAsync(product);
	}

	public async Task<bool> UpdateProduct(Product product)
	{
		var update = await _context.Products.ReplaceOneAsync(filter: p => p.Id == product.Id, replacement: product);
		return update.IsAcknowledged && update.ModifiedCount > 0;
	}

	public async Task<bool> DeleteProduct(string id)
	{
		var delete = await _context.Products.DeleteOneAsync(filter: p => p.Id == id);
		return delete.IsAcknowledged && delete.DeletedCount > 0;
	}
}