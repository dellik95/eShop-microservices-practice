namespace Catalog.API.Data;

public class MongoDBSettings
{
	public static string Key = "MongoSettings";

	public string ConnectionsString { get; set; }
	public string DatabaseName { get; set; }
	public string CollectionName { get; set; }
}