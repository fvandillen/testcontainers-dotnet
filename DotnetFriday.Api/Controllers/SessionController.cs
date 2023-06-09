using DotnetFriday.Domain;
using DotnetFriday.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetFriday.Api.Controllers;

public class SessionController : Controller
{
	private readonly DotnetFridayService _service;

	public SessionController(DotnetFridayService service)
	{
		_service = service;
	}
	
	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetSession(Guid id)
	{
		var result = await _service.GetSession(id);
		return Ok(result);
	}

	// [HttpPost]
	// public async Task<IActionResult> RegisterSession(Session session)
	// {
	// 	// var result = await _service.Register(attendee);
	// 	// return Ok(result);
	// }
}