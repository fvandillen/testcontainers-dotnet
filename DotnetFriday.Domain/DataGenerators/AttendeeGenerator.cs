using Bogus;
using DotnetFriday.Domain.Models;

namespace DotnetFriday.Domain.DataGenerators;

public static class AttendeeGenerator
{
	public static Faker<Attendee> GetGenerator(List<Event> events)
	{
		return new Faker<Attendee>()
			.RuleFor(x => x.Id, _ => Guid.NewGuid())
			.RuleFor(x => x.Name, f => f.Name.FullName())
			.RuleFor(x => x.Email, f => f.Person.Email)
			.RuleFor(x => x.Company, f => f.Company.CompanyName())
			.RuleFor(x => x.JobTitle, f => f.Name.JobTitle())
			.RuleFor(x => x.Event, f => f.PickRandom(events));
		
	}
}