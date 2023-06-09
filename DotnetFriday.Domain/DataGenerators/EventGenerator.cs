using Bogus;
using DotnetFriday.Domain.Models;

namespace DotnetFriday.Domain.DataGenerators;

public static class EventGenerator
{
	public static Faker<Event> GetGenerator()
	{
		return new Faker<Event>()
			.RuleFor(x => x.Id, _ => Guid.NewGuid())
			.RuleFor(x => x.Date, f => f.Date.Between(DateTime.Today, DateTime.Today.AddMonths(6)))
			.RuleFor(x => x.VenueCapacity, f => f.Random.Int(25, 50))
			.RuleFor(x => x.Food, f => f.PickRandom<FoodType>())
			.RuleFor(x => x.Attendees, (f, a) => AttendeeGenerator.GetGenerator(new List<Event> { a } ).Generate(a.VenueCapacity));
	}
}