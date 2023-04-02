using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web.Resource;
using Repositories.Context;
using Repositories.Models;
using System;

namespace LocallyProAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    //[Authorize]
    //[AllowAnonymous]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EventsController(ApplicationDBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("")]
        public async Task<IEnumerable<Event>> Index()
        {
            //var userID = _userManager.GetUserId(User);
            //ApplicationUser user = await _userManager.GetUserAsync(User);
            return await _context.Event.ToListAsync();
        }
        /*
        [Route("[action]/city={City}&address={Address}&dateTimeStart={DateTimeStart}&dateTimeEnd={DateTimeEnd}")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<Event?> Create([Bind("Id,City,Address,DateTimeStart,DateTimeEnd")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return @event;
            }
            return null;
        }
        */
        [HttpPost]
        public async Task<ActionResult<Event>> Add(Event @event)
        {
            _context.Event.Add(@event);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("Events");
            //return CreatedAtAction(nameof(Event), new { id = @event.Id }, @event);
            return Ok(@event);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Event>> DeleteEvent(int id)
        {
            try
            {
                var eventToDelete = await _context.Event.FindAsync(id);


                if (eventToDelete == null)
                {
                    
                    return NotFound($"Event with Id = {id} not found");
                }

                _context.Event.Remove(eventToDelete);
                await _context.SaveChangesAsync();
                return Ok(eventToDelete);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
        /*
        // GET: Events/Delete/
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Event == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }*/
    }
}
