using Discount.Grpc;
using Discount.Grpc.Repositories;
using Discount.Grpc.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<PostgresSettings>(builder.Configuration.GetSection(PostgresSettings.Key));
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddAutoMapper(typeof(AssemblyReference).Assembly);

builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<DiscountService>();
app.MapGet("/",
	() =>
		"Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();