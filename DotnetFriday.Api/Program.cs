using DotnetFriday.Domain;
using DotnetFriday.Domain.DataGenerators;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json")
	.Build();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database.
builder.Services.AddDbContext<DotnetFridayContext>(options => options.UseSqlServer(config["Database:ConnectionString"]));

builder.Services.AddScoped<DotnetFridayService>();

var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
	var context = scope.ServiceProvider.GetRequiredService<DotnetFridayContext>();
	context.Database.EnsureDeleted();
	context.Database.EnsureCreated();

	if (config["Database:GenerateTestData"] == "true")
	{
		var events = EventGenerator.GetGenerator().Generate(3);
		context.Events.AddRange(events);
		context.SaveChanges();
	
		var sessions = SessionGenerator.GetGenerator(events).Generate(10);
		context.Sessions.AddRange(sessions);
		context.SaveChanges();
	}
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Make public partial to allow integration testing.
public partial class Program { }