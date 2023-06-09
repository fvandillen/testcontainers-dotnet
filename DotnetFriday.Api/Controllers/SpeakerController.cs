using DotnetFriday.Domain;
using DotnetFriday.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetFriday.Api.Controllers;

[Route("speaker")]
public class SpeakerController : Controller
{
	private readonly DotnetFridayService _service;

	public SpeakerController(DotnetFridayService service)
	{
		_service = service;
	}
	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetSpeaker(Guid id)
	{
		var result = await _service.GetSpeaker(id);
		return Ok(result);
	}

	[HttpPost]
	public async Task<IActionResult> CreateSpeaker(Speaker speaker)
	{
		// Call service.
		
		return CreatedAtAction(nameof(GetSpeaker), speaker);
	}
}