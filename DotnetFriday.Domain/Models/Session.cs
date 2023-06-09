namespace DotnetFriday.Domain.Models;

public class Session : ModelBase
{
	public Speaker Speaker { get; set; }
	public Event Event { get; set; }
	public int DurationMinutes { get; set; }
}