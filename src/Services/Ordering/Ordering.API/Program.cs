using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddApplicationsLayer();
builder.Services.AddInfrastructureLayer(builder.Configuration);
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(options =>
	{
		options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
	});
}

app.MapControllers();
app.Run();