using Bogus;
using DotnetFriday.Domain.Models;

namespace DotnetFriday.Domain.DataGenerators;

public static class SessionGenerator
{
	public static Faker<Session> GetGenerator(List<Event> events)
	{
		return new Faker<Session>()
			.RuleFor(x => x.Id, _ => Guid.NewGuid())
			.RuleFor(x => x.DurationMinutes, f => f.Random.Int(15, 60))
			.RuleFor(x => x.Event, f => f.PickRandom(events))
			.RuleFor(x => x.Speaker, (f,s) => 
				SpeakerGenerator.GetGenerator(new List<Session> {s}).Generate(1).Single());
	}
}