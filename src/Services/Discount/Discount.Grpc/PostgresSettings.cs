namespace Discount.Grpc;

public class PostgresSettings
{
	public static string? Key => nameof(PostgresSettings);

	public string? ConnectionString { get; set; }
}