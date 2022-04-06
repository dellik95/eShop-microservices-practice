using System.Reflection;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Discount.Grpc.Protos;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(o =>
{
	var connectionString = builder.Configuration["DiscountGrpcSettings:DiscountUrl"];
	o.Address = new Uri(connectionString);
});
builder.Services.AddScoped<DiscountGrpcService>();

builder.Services.AddStackExchangeRedisCache(options =>
{
	options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionsString");
});

builder.Services.AddMassTransit(o =>
{
	o.UsingRabbitMq((ctx, cnf) => { cnf.Host(builder.Configuration["EventBusSettings:HostAddress"]); });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.Run();