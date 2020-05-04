using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BikeRental.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using BikeRental.MVCUI.Models.ViewModels;

namespace BikeRental.MVCUI.Controllers
{
    public class UsersController : Controller
    {
        private readonly BikeRentalContext _context;

        public UsersController(BikeRentalContext context)
        {
            _context = context;
        }

        string baseurl = "https://localhost:44369/api/";
        // GET: Users
        public async Task<IActionResult> Index()
        {
            List<Location> locationList = new List<Location>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{baseurl}Locations"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    locationList = JsonConvert.DeserializeObject<List<Location>>(apiResponse);
                }
            }
            return View(locationList);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LocationBicycleAccessoriesViewModel lbavm = new LocationBicycleAccessoriesViewModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{baseurl}bicycles"))
                {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                    lbavm.Bicycle = JsonConvert.DeserializeObject<List<Bicycle>>(apiresponse);

                }

                using (var response = await httpClient.GetAsync($"{baseurl}accessories"))
                {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                    lbavm.Accessories = JsonConvert.DeserializeObject<List<Accessories>>(apiresponse);

                }
                using (var response = await httpClient.GetAsync($"{baseurl}Locations/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    lbavm.Location = JsonConvert.DeserializeObject<Location>(apiResponse);
                }
            }
            lbavm.Bicycle = lbavm.Bicycle.Where(b => b.LocationId == id).ToList();

            return View(lbavm);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Email");
            ViewData["LocationId"] = new SelectList(_context.Location, "Id", "City");
            ViewData["TypeId"] = new SelectList(_context.ReservationType, "Id", "Type");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OutTime,Price,CustomerId,LocationId,TypeId")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Email", reservation.CustomerId);
            ViewData["LocationId"] = new SelectList(_context.Location, "Id", "City", reservation.LocationId);
            ViewData["TypeId"] = new SelectList(_context.ReservationType, "Id", "Type", reservation.TypeId);
            return View(reservation);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Email", reservation.CustomerId);
            ViewData["LocationId"] = new SelectList(_context.Location, "Id", "City", reservation.LocationId);
            ViewData["TypeId"] = new SelectList(_context.ReservationType, "Id", "Type", reservation.TypeId);
            return View(reservation);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OutTime,Price,CustomerId,LocationId,TypeId")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Email", reservation.CustomerId);
            ViewData["LocationId"] = new SelectList(_context.Location, "Id", "City", reservation.LocationId);
            ViewData["TypeId"] = new SelectList(_context.ReservationType, "Id", "Type", reservation.TypeId);
            return View(reservation);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .Include(r => r.Customer)
                .Include(r => r.Location)
                .Include(r => r.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservation.FindAsync(id);
            _context.Reservation.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservation.Any(e => e.Id == id);
        }
    }
}
