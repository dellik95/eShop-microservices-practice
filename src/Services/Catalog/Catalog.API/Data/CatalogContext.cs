using Catalog.API.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using static Catalog.API.Data.CatalogContextSeed;

namespace Catalog.API.Data;

public class CatalogContext : ICatalogContext
{
	private readonly MongoDBSettings _options;
	private readonly IMongoDatabase _database;
	private readonly MongoClient _client;

	public CatalogContext(IOptionsMonitor<MongoDBSettings> optionsMonitor)
	{
		_options = optionsMonitor.CurrentValue;
		_client = new MongoClient(_options.ConnectionsString);
		_database = _client.GetDatabase(_options.DatabaseName);
		Seed(Products);
	}


	public IMongoClient Client => _client;
	public IMongoDatabase Database => _database;

	public IMongoCollection<Product> Products => _database.GetCollection<Product>(_options.CollectionName);
}