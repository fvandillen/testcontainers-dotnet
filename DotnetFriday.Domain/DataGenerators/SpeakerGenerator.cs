using Bogus;
using DotnetFriday.Domain.Models;

namespace DotnetFriday.Domain.DataGenerators;

public class SpeakerGenerator
{
	public static Faker<Speaker> GetGenerator(List<Session> sessions)
	{
		return new Faker<Speaker>()
			.RuleFor(x => x.Id, _ => Guid.NewGuid())
			.RuleFor(x => x.Name, f => f.Name.FullName())
			.RuleFor(x => x.Session, f => f.PickRandom(sessions));
	}
}