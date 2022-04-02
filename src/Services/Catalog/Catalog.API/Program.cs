using Catalog.API.Data;
using Catalog.API.Repositories;

namespace Catalog.API
{
	class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();


			builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection(MongoDBSettings.Key));
			builder.Services.AddScoped<ICatalogContext, CatalogContext>();
			builder.Services.AddScoped<IProductRepository, ProductRepository>();

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			
			app.UseAuthorization();
			app.MapControllers();
			app.Run();
		}
	}
}