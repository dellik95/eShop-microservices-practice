using Dapper;
using Discount.API.Entities;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Discount.API.Repositories;

public class DiscountRepository : IDiscountRepository, IDisposable
{
	private readonly PostgresSettings _options;

	private NpgsqlConnection _connection;

	public DiscountRepository(IOptions<PostgresSettings> options)
	{
		_options = options.Value;
		_connection = new NpgsqlConnection(_options.ConnectionString);
	}

	public async Task<Coupon> GetDiscount(string productName)
	{
		var coupon =
			await _connection.QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM Coupon WHERE ProductName=@ProductName",
				new
				{
					ProductName = productName
				});

		return coupon ?? new Coupon()
		{
			Amount = 0,
			Description = "No Discount",
			ProductName = "No Discount"
		};
	}

	public async Task<bool> CreateDiscount(Coupon coupon)
	{
		var affected = await _connection.ExecuteAsync(
			"INSERT INTO Coupon (ProductName, Description, Amount) VALUES(@ProductName, @Description, @Amount)",
			new
			{
				ProductName = coupon.ProductName,
				Description = coupon.Description,
				Amount = coupon.Amount
			});
		return affected > 0;
	}

	public async Task<bool> UpdateDiscount(Coupon coupon)
	{
		var affected = await _connection.ExecuteAsync(
			"UPDATE Coupon set ProductName = @ProductName, Description = @Description, Amount = @Amount where Id=@Id",
			new
			{
				ProductName = coupon.ProductName,
				Description = coupon.Description,
				Amount = coupon.Amount,
				Id = coupon.Id
			});
		return affected > 0;
	}

	public async Task<bool> DeleteDiscount(string productName)
	{
		var affected = await _connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName=@ProductName", new
		{
			ProductName = productName
		});
		return affected > 0;
	}

	void IDisposable.Dispose()
	{
		_connection.Dispose();
	}
}