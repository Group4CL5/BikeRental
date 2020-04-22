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
    public class BikeAccessoriesController : ControllerBase
    {
        private readonly BikeRentalContext _context;

        public BikeAccessoriesController()
        {
            _context = new BikeRentalContext();
        }

        // GET: api/BikeAccessories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BikeAccessories>>> GetBikeAccessories()
        {
            return await _context.BikeAccessories.ToListAsync();
        }

        // GET: api/BikeAccessories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BikeAccessories>> GetBikeAccessories(int id)
        {
            var bikeAccessories = await _context.BikeAccessories.FindAsync(id);

            if (bikeAccessories == null)
            {
                return NotFound();
            }

            return bikeAccessories;
        }

        // PUT: api/BikeAccessories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBikeAccessories(int id, BikeAccessories bikeAccessories)
        {
            if (id != bikeAccessories.Id)
            {
                return BadRequest();
            }

            _context.Entry(bikeAccessories).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BikeAccessoriesExists(id))
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

        // POST: api/BikeAccessories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<BikeAccessories>> PostBikeAccessories(BikeAccessories bikeAccessories)
        {
            _context.BikeAccessories.Add(bikeAccessories);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBikeAccessories", new { id = bikeAccessories.Id }, bikeAccessories);
        }

        // DELETE: api/BikeAccessories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BikeAccessories>> DeleteBikeAccessories(int id)
        {
            var bikeAccessories = await _context.BikeAccessories.FindAsync(id);
            if (bikeAccessories == null)
            {
                return NotFound();
            }

            _context.BikeAccessories.Remove(bikeAccessories);
            await _context.SaveChangesAsync();

            return bikeAccessories;
        }

        private bool BikeAccessoriesExists(int id)
        {
            return _context.BikeAccessories.Any(e => e.Id == id);
        }
    }
}
