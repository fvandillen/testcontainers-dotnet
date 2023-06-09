namespace DotnetFriday.Domain.Models;

public class Speaker : ModelBase
{
	public string? Name { get; set; }
	public Session Session;
}