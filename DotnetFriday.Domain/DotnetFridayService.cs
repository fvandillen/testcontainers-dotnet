using DotnetFriday.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetFriday.Domain;

public class DotnetFridayService
{
	private readonly DotnetFridayContext _context;

	public DotnetFridayService(DotnetFridayContext context)
	{
		_context = context;
	}

	public async Task<Event?> GetEvent(Guid id)
	{
		return await _context.Events.FindAsync(id);
	}

	public async Task<IEnumerable<Event>> GetAllEvents()
	{
		return await _context.Events.ToListAsync();
	}

	public async Task<Event> CreateEvent(Event @event)
	{
		if (@event.VenueCapacity < 25) 
			throw new Exception("The capacity of the event must be at least 25 seats!");

		var previousEvent = await _context.Events
			.Where(x => x.Date < @event.Date)
			.OrderByDescending(x => x.Date)
			.FirstOrDefaultAsync();

		if (previousEvent != null && previousEvent.Food == @event.Food) 
			throw new Exception("That food was also served at the last edition! Let's choose something else.");

		await _context.Events.AddAsync(@event);
		await _context.SaveChangesAsync();
		
		return @event;
	}

	public async Task<Speaker?> GetSpeaker(Guid id)
	{
		return await _context.Speakers.FindAsync(id);
	}

	public async Task NewSession(Session session)
	{
		if (session.Speaker == null) throw new Exception("A session must have a speaker!");
		if (session.Event.Date < DateTime.Today) throw new Exception("You cannot add a session to a past event!");

		await _context.Sessions.AddAsync(session);
		await _context.SaveChangesAsync();
	}
	
	public async Task<Attendee> RegisterAttendee(Attendee attendee)
	{
		if (attendee.Event == null) throw new Exception("You must register to a specific Event!");
		if (attendee.Event.Date < DateTime.Today) 
			throw new Exception("You cannot register to a past event!");
		if (attendee.Event.Attendees.Any(x => x.Email == attendee.Email)) 
			throw new Exception("You already registered to this event!");

		await _context.Attendees.AddAsync(attendee);
		await _context.SaveChangesAsync();

		return attendee;
	}
	
	public async Task<Attendee?> GetAttendee(Guid id)
	{
		return await _context.Attendees.FindAsync(id);
	}

	public async Task<Session?> GetSession(Guid id)
	{
		return await _context.Sessions.FindAsync(id);
	}
	// public async Task CancelAttendeeRegistration(Attendee attendee)
	// {
	// 	
	// }
}