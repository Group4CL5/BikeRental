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
    public class ReservationAccessoriesController : ControllerBase
    {
        private readonly BikeRentalContext _context;

        public ReservationAccessoriesController()
        {
            _context = new BikeRentalContext();
        }

        // GET: api/ReservationAccessories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationAccessories>>> GetReservationAccessorie()
        {
            return await _context.ReservationAccessories.ToListAsync();
        }

        // GET: api/ReservationAccessories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationAccessories>> GetReservationAccessorie(int id)
        {
            var reservationAccessorie = await _context.ReservationAccessories.FindAsync(id);

            if (reservationAccessorie == null)
            {
                return NotFound();
            }

            return reservationAccessorie;
        }

        // PUT: api/ReservationAccessories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservationAccessorie(int id, ReservationAccessories reservationAccessories)
        {
            if (id != reservationAccessories.Id)
            {
                return BadRequest();
            }

            _context.Entry(reservationAccessories).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationAccessorieExists(id))
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

        // POST: api/ReservationAccessories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ReservationAccessories>> PostReservationAccessorie(ReservationAccessories reservationAccessories)
        {
            _context.ReservationAccessories.Add(reservationAccessories);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservationAccessorie", new { id = reservationAccessories.Id }, reservationAccessories);
        }

        // DELETE: api/ReservationAccessories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ReservationAccessories>> DeleteReservationAccessorie(int id)
        {
            var reservationAccessorie = await _context.ReservationAccessories.FindAsync(id);
            if (reservationAccessorie == null)
            {
                return NotFound();
            }

            _context.ReservationAccessories.Remove(reservationAccessorie);
            await _context.SaveChangesAsync();

            return reservationAccessorie;
        }

        private bool ReservationAccessorieExists(int id)
        {
            return _context.ReservationAccessories.Any(e => e.Id == id);
        }
    }
}
