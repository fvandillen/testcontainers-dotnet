using DotnetFriday.Domain.DataGenerators;
using DotnetFriday.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DotnetFriday.Domain;

public class DotnetFridayContext : DbContext
{
	public DbSet<Event> Events { get; set; }
	public DbSet<Session> Sessions{ get; set; }
	public DbSet<Speaker> Speakers{ get; set; }
	public DbSet<Attendee> Attendees { get; set; }
	
	private IConfiguration _configuration;
	
	public DotnetFridayContext(DbContextOptions<DotnetFridayContext> options, IConfiguration configuration) : base(options)
	{
		_configuration = configuration;
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		if (_configuration["Database:GenerateTestData"] == "true")
		{
			// var events = EventGenerator.GetGenerator().Generate(3);
			// modelBuilder.Entity<Event>()
			// 	.HasData(events);
			// modelBuilder.Entity<Event>().HasMany(x => x.Attendees);
			// modelBuilder.Entity<Event>().HasMany(x => x.Sessions);
			
			// var sessions = SessionGenerator.GetGenerator(events).Generate(10);
			// modelBuilder.Entity<Session>().HasData(sessions);

			// var attendees = AttendeeGenerator.GetGenerator(events).Generate(150);
			// modelBuilder.Entity<Attendee>().HasData(attendees);
		}
	}
}