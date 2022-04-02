using System.Linq.Expressions;
using Catalog.API.Entities;

namespace Catalog.API.Repositories;

public interface IProductRepository
{
	Task<IEnumerable<Product>> GetProductsByConditions(Expression<Func<Product, bool>>? expression);
	Task<Product> GetProductByConditions(Expression<Func<Product, bool>> expression);


	Task CreateProduct(Product product);
	Task<bool> UpdateProduct(Product product);
	Task<bool> DeleteProduct(string id);
}