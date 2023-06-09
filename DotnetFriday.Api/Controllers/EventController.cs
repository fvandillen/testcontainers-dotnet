using DotnetFriday.Domain;
using DotnetFriday.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetFriday.Api.Controllers;

[Route("event")]
public class EventController : Controller
{
	private readonly DotnetFridayService _service;

	public EventController(DotnetFridayService service)
	{
		_service = service;
	}

	[HttpGet("{id:guid}")]
	public async Task<Event?> GetEvent(Guid id)
	{
		var result = await _service.GetEvent(id);
		return result;
	}
	
	[HttpGet("list")]
	public async Task<IEnumerable<Event>> GetAllEvents()
	{
		var result = await _service.GetAllEvents();
		return result;
	}

	[HttpPost]
	public async Task<IActionResult> CreateEvent([FromBody]Event @event)
	{
		var result = await _service.CreateEvent(@event);
		return Ok(result);
	}
}