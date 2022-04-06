using System.Reflection;
using EventBus.Messages.Common;
using MassTransit;
using Ordering.API.EventBusConsumers;
using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddApplicationsLayer();
builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddMassTransit(o =>
{
	o.AddConsumer<BasketCheckoutConsumer>();
	o.UsingRabbitMq((context, configurator) =>
	{
		configurator.Host(builder.Configuration["EventBusSettings:HostAddress"]);
		configurator.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue,
			c => c.ConfigureConsumer<BasketCheckoutConsumer>(context));
	});
});
builder.Services.AddScoped<BasketCheckoutConsumer>();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); });
}

app.MapControllers();
app.Run();