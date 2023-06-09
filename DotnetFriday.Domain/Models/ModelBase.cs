using System.ComponentModel.DataAnnotations;

namespace DotnetFriday.Domain.Models;

public abstract class ModelBase
{
	[Key]
	public Guid Id { get; set; }
}