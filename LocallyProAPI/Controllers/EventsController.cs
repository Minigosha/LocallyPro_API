using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web.Resource;
using Repositories.Context;
using Repositories.Models;
using System;
using Microsoft.AspNetCore.JsonPatch;


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

        [Authorize(Policy = "AuthZPolicy")]
        [HttpGet("")]
        public async Task<IEnumerable<Event>> Index()
        {
            //var userID = _userManager.GetUserId(User);
            //ApplicationUser user = await _userManager.GetUserAsync(User);
            return await _context.Event.ToListAsync();
        }

        [HttpGet("{id:int}")]
        // GET: Events/Edit/5
        public async Task<IActionResult> Get(int? id)
        {


            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event.FindAsync(id);

            if (@event == null)
            {
                return NotFound();
            }

            return Ok(@event);

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
        [Authorize]
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

        [HttpPatch("{id:int}")]
        // GET: Events/Edit/5
        public async Task<ActionResult<Event>> Patch(int? id, [FromBody] Event @event)
        {
          

            if (id == null || _context.Event == null)
            {
                return NotFound();
            }

             

            if (@event == null)
            {
                return NotFound();
            }
            //return Ok(@event);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
            }
            return @event;

        }


        // POST: Event/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       /* [HttpPatch("{id:int}")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]

        
        public async Task<IActionResult> Patch(int id, [Bind("Id, City, Address, DateTimeStart, DateTimeEnd")] Event @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return (IActionResult)@event;
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

        private bool EventExists(int id)
        {
            return _context.Producer.Any(e => e.Id == id);
        }
    }
}
