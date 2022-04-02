namespace Discount.API;

public class PostgresSettings
{
	public static string Key = nameof(PostgresSettings);

	public string ConnectionString { get; set; }
}