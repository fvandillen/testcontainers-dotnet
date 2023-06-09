using DotnetFriday.Domain;
using DotnetFriday.Domain.DataGenerators;
using DotnetFriday.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetFriday.Api.Controllers;

[Route("attendee")]
public class AttendeeController : Controller
{
	private readonly DotnetFridayService _service;

	public AttendeeController(DotnetFridayService service)
	{
		_service = service;
	}
	
	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetAttendee(Guid id)
	{
		var result = await _service.GetAttendee(id);
		return Ok(result);
	}

	[HttpPost]
	public async Task<IActionResult> RegisterAttendee(Attendee attendee)
	{
		var result = await _service.RegisterAttendee(attendee);
		return Ok(result);
	}
}