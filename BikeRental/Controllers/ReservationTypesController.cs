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
    public class ReservationTypesController : ControllerBase
    {
        private readonly BikeRentalContext _context;

        public ReservationTypesController()
        {
            _context = new BikeRentalContext();
        }

        // GET: api/ReservationTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationType>>> GetReservationType()
        {
            return await _context.ReservationType.ToListAsync();
        }

        // GET: api/ReservationTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationType>> GetReservationType(int id)
        {
            var reservationType = await _context.ReservationType.FindAsync(id);

            if (reservationType == null)
            {
                return NotFound();
            }

            return reservationType;
        }

        // PUT: api/ReservationTypes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservationType(int id, ReservationType reservationType)
        {
            if (id != reservationType.Id)
            {
                return BadRequest();
            }

            _context.Entry(reservationType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationTypeExists(id))
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

        // POST: api/ReservationTypes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ReservationType>> PostReservationType(ReservationType reservationType)
        {
            _context.ReservationType.Add(reservationType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservationType", new { id = reservationType.Id }, reservationType);
        }

        // DELETE: api/ReservationTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ReservationType>> DeleteReservationType(int id)
        {
            var reservationType = await _context.ReservationType.FindAsync(id);
            if (reservationType == null)
            {
                return NotFound();
            }

            _context.ReservationType.Remove(reservationType);
            await _context.SaveChangesAsync();

            return reservationType;
        }

        private bool ReservationTypeExists(int id)
        {
            return _context.ReservationType.Any(e => e.Id == id);
        }
    }
}
