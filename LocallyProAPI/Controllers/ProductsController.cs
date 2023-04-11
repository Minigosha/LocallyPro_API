using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.Context;
using Repositories.Models;

namespace LocallyProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductsController(ApplicationDBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("")]
        public async Task<IEnumerable<Product>> Index()
        {            
            return await _context.Product.ToListAsync();
        }

        [HttpGet("{id:int}")]
        // GET: Products/Edit/5
        public async Task<IActionResult> Get(int? id)
        {


            if (id == null)
            {
                return NotFound();
            }

            var @product = await _context.Product.FindAsync(id);

            if (@product == null)
            {
                return NotFound();
            }

            return Ok(@product);

        }



        [HttpPost]
        public async Task<ActionResult<Product>> Add(Product @product)
        {
            _context.Product.Add(@product);
            await _context.SaveChangesAsync();

            return Ok(@product);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            try
            {
                var productToDelete = await _context.Product.FindAsync(id);


                if (productToDelete == null)
                {

                    return NotFound($"Product with Id = {id} not found");
                }

                _context.Product.Remove(productToDelete);
                await _context.SaveChangesAsync();
                return Ok(productToDelete);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }

            //product.Producer = (await _userManager.GetUserAsync(User)).Producer;
            _context.Producer.Single(p => p.Id == 1);
        }

        [HttpPatch("{id:int}")]
        // GET: Product/Edit/5
        public async Task<ActionResult<Product>> Patch(int? id, [FromBody] Product @product)
        {


            if (id == null || _context.Product == null)
            {
                return NotFound();
            }



            if (@product == null)
            {
                return NotFound();
            }
        

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(@product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            return @product;

        }




        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}




