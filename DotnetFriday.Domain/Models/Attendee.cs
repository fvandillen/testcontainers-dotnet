using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetFriday.Domain.Models;

public class Attendee : ModelBase
{
	public String Name { get; set; }
	public String Email { get; set; }
	public String Company { get; set; }
	public String JobTitle { get; set; }
	public Event Event { get; set; }
}