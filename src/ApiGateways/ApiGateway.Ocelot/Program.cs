using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
	.AddJsonFile("ocelot.json")
	.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", true, true);
builder.Logging.AddConsole().AddDebug();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOcelot().AddCacheManager(opt =>
{
	opt.WithDictionaryHandle();
});


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.MapControllers();

await app.UseOcelot();

app.Run();