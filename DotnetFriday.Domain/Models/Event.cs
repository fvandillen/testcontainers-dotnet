namespace DotnetFriday.Domain.Models;

public class Event : ModelBase
{
	public DateTime Date { get; set; }
	public List<Session> Sessions { get; set; } = new List<Session>();
	public List<Attendee> Attendees { get; set; } = new List<Attendee>();
	public int VenueCapacity { get; set; }
	public FoodType Food { get; set; }
}

public enum FoodType
{
	Pizza,
	Chinese,
	American,
	Indonesian
}