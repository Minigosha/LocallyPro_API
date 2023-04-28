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
        //private readonly IBufferedFileUploadService _bufferedFileUploadService;

        // private readonly UserManager<ApplicationUser> _userManager;

        public ProducersController(ApplicationDBContext context /*IBufferedFileUploadService bufferedFileUploadService/*, UserManager<ApplicationUser> userManager*/)
        {
            _context = context;
            //  _userManager = userManager;
            //_bufferedFileUploadService = bufferedFileUploadService;
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
        /*
        public async Task<IActionResult> Create(IFormFile file)
        {
            try
            {
                if (await _bufferedFileUploadService.UploadFile(file))
                {
                    ViewBag.Message = "File Upload Successful";
                }
                else
                {
                    ViewBag.Message = "File Upload Failed";
                }
            }
            catch (Exception ex)
            {
                //Log ex
                ViewBag.Message = "File Upload Failed";
            }


            product.Producer = (await _userManager.GetUserAsync(User)).Producer;
            //_context.Producer.Single(p => p.Id == 1);

            product.ImageName = file.FileName;
            ModelState.Clear();
            TryValidateModel(product);
            if (ModelState.IsValid)
            {

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        */



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
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
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
