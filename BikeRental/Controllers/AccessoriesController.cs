using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BikeRental.Models;

namespace BikeRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessoriesController : ControllerBase
    {
        private readonly BikeRentalContext _context;

        public AccessoriesController()
        {
            _context = new BikeRentalContext();
        }

        // GET: api/Accessories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Accessories>>> GetAccessories()
        {
            return await _context.Accessories.ToListAsync();
        }

        // GET: api/Accessories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Accessories>> GetAccessories(int id)
        {
            var accessories = await _context.Accessories.FindAsync(id);

            if (accessories == null)
            {
                return NotFound();
            }

            return accessories;
        }

        // PUT: api/Accessories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccessories(int id, Accessories accessories)
        {
            if (id != accessories.Id)
            {
                return BadRequest();
            }

            _context.Entry(accessories).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccessoriesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Accessories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Accessories>> PostAccessories(Accessories accessories)
        {
            _context.Accessories.Add(accessories);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccessories", new { id = accessories.Id }, accessories);
        }

        // DELETE: api/Accessories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Accessories>> DeleteAccessories(int id)
        {
            var accessories = await _context.Accessories.FindAsync(id);
            if (accessories == null)
            {
                return NotFound();
            }

            _context.Accessories.Remove(accessories);
            await _context.SaveChangesAsync();

            return accessories;
        }

        private bool AccessoriesExists(int id)
        {
            return _context.Accessories.Any(e => e.Id == id);
        }
    }
}
