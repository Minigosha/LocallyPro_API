using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.Context;
using Repositories.Models;
using System;

namespace LocallyProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducersController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
       // private readonly UserManager<ApplicationUser> _userManager;

        public ProducersController(ApplicationDBContext context/*, UserManager<ApplicationUser> userManager*/)
        {
            _context = context;
          //  _userManager = userManager;
        }


        [HttpGet("")]
        public async Task<IEnumerable<Producer>> Index()
        {
           
            return await _context.Producer.ToListAsync();
        }

        [HttpGet("{id:int}")]
        // GET: Producers/Edit/5
        public async Task<IActionResult> Get(int? id)
        {


            if (id == null)
            {
                return NotFound();
            }

            var @producer = await _context.Producer.FindAsync(id);

            if (@producer == null)
            {
                return NotFound();
            }

            return Ok(@producer);

        }


        [HttpPost]
        public async Task<ActionResult<Producer>> Add(Producer @producer)
        {
            _context.Producer.Add(@producer);
            await _context.SaveChangesAsync();
                        
            return Ok(@producer);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Producer>> DeleteProducer(int id)
        {
            try
            {
                var producerToDelete = await _context.Producer.FindAsync(id);


                if (producerToDelete == null)
                {

                    return NotFound($"Producer with Id = {id} not found");
                }

                _context.Producer.Remove(producerToDelete);
                await _context.SaveChangesAsync();
                return Ok(producerToDelete);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }

        [HttpPatch("{id:int}")]
        // GET: Producer/Edit/5
        public async Task<ActionResult<Producer>> Patch(int? id, [FromBody] Producer @producer)
        {


            if (id == null || _context.Producer == null)
            {
                return NotFound();
            }



            if (@producer == null)
            {
                return NotFound();
            }
           

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@producer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProducerExists(@producer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            return @producer;

        }
        private bool ProducerExists(int id)
        {
            return _context.Producer.Any(e => e.Id == id);
        }



    }
}
