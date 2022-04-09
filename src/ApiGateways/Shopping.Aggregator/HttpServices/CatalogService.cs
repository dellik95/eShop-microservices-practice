using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.HttpServices;

public class CatalogService : ICatalogService
{
	private readonly HttpClient _client;

	public CatalogService(HttpClient client)
	{
		_client = client;
	}

	public async Task<IEnumerable<Catalog>> GetCatalog()
	{
		var response = await _client.GetAsync("/api/v1/Catalog");
		var catalogData = await response.ReadContentAs<List<Catalog>>();
		return catalogData;
	}

	public async Task<IEnumerable<Catalog>> GetCatalogByCategory(string category)
	{
		var response = await _client.GetAsync($"/api/v1/Catalog/GetProductByCategory/{category}");
		var catalogData = await response.ReadContentAs<List<Catalog>>();
		return catalogData;
	}

	public async Task<Catalog> GetCatalog(string id)
	{
		var response = await _client.GetAsync($"/api/v1/Catalog/{id}");
		var catalogData = await response.ReadContentAs<Catalog>();
		return catalogData;
	}
}