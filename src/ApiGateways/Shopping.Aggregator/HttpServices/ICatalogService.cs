using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.HttpServices;

public interface ICatalogService
{
	Task<IEnumerable<Catalog>> GetCatalog();
	Task<IEnumerable<Catalog>> GetCatalogByCategory(string category);
	Task<Catalog> GetCatalog(string id);
}