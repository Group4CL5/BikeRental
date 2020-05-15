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
    public class BikesReservedsController : ControllerBase
    {
        private readonly BikeRentalContext _context;

        public BikesReservedsController()
        {
            _context = new BikeRentalContext();
        }

        // GET: api/BikesReserveds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BikesReserved>>> GetBikesReserved()
        {
            return await _context.BikesReserved.ToListAsync();
        }

        // GET: api/BikesReserveds/BikeObject
        [HttpGet]
        [Route("BikeObjects/{id}")]
        public async Task<ActionResult<IEnumerable<BikesReserved>>> BikeObjects(int id)
        {
            List<BikesReserved> br = await _context.BikesReserved.Where(b => b.ReservationId == id).ToListAsync();

            return br;
        }



        // GET: api/BikesReserveds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BikesReserved>> GetBikesReserved(int id)
        {
            var bikesReserved = await _context.BikesReserved.FindAsync(id);

            if (bikesReserved == null)
            {
                return NotFound();
            }

            return bikesReserved;
        }

        // PUT: api/BikesReserveds/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBikesReserved(int id, BikesReserved bikesReserved)
        {
            if (id != bikesReserved.Id)
            {
                return BadRequest();
            }

            _context.Entry(bikesReserved).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BikesReservedExists(id))
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

        // POST: api/BikesReserveds
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<BikesReserved>> PostBikesReserved(BikesReserved bikesReserved)
        {
            _context.BikesReserved.Add(bikesReserved);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBikesReserved", new { id = bikesReserved.Id }, bikesReserved);
        }

        // DELETE: api/BikesReserveds/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BikesReserved>> DeleteBikesReserved(int id)
        {
            var bikesReserved = await _context.BikesReserved.FindAsync(id);
            if (bikesReserved == null)
            {
                return NotFound();
            }

            _context.BikesReserved.Remove(bikesReserved);
            await _context.SaveChangesAsync();

            return bikesReserved;
        }

        private bool BikesReservedExists(int id)
        {
            return _context.BikesReserved.Any(e => e.Id == id);
        }
    }
}
