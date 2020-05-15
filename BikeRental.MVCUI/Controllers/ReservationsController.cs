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
    
    public class ReservationsController : Controller
    {
        string baseurl = "https://localhost:44369/api/";
        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            List<Reservation> reservationList = new List<Reservation>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{baseurl}Reservations"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<List<Reservation>>(apiResponse);
                }
                foreach (var item in reservationList)
                {
                    using (var response = await httpClient.GetAsync($"{baseurl}Locations/{item.LocationId}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        item.Location = JsonConvert.DeserializeObject<Location>(apiResponse);

                    }
                    using (var response = await httpClient.GetAsync($"{baseurl}Customers/{item.CustomerId}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        item.Customer = JsonConvert.DeserializeObject<Customer>(apiResponse);

                    }
                }
            }
            return View(reservationList);
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ReservationBikeViewModel rbvm = new ReservationBikeViewModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync($"Reservations/{id}");
                if (res.IsSuccessStatusCode)
                {
                    var response = res.Content.ReadAsStringAsync().Result;
                    Reservation reservation = JsonConvert.DeserializeObject<Reservation>(response);
                    using (res = await client.GetAsync($"{baseurl}Locations/{reservation.LocationId}"))
                    {
                        string apiResponse = await res.Content.ReadAsStringAsync();
                        reservation.Location = JsonConvert.DeserializeObject<Location>(apiResponse);

                    }
                    using (res = await client.GetAsync($"{baseurl}ReservationTypes/{reservation.TypeId}"))
                    {
                        string apiResponse = await res.Content.ReadAsStringAsync();
                        reservation.Type = JsonConvert.DeserializeObject<ReservationType>(apiResponse);

                    }
                    using (res = await client.GetAsync($"{baseurl}Customers/{reservation.CustomerId}"))
                    {
                        string apiResponse = await res.Content.ReadAsStringAsync();
                        reservation.Customer = JsonConvert.DeserializeObject<Customer>(apiResponse);

                    }
                    rbvm.Reservation = reservation;
                    using (res = await client.GetAsync($"{baseurl}BikesReserveds/BikeObjects/{reservation.Id}"))
                    {
                        string apiResponse = await res.Content.ReadAsStringAsync();
                        rbvm.BikesReserved = JsonConvert.DeserializeObject<List<BikesReserved>>(apiResponse);

                    }
                    foreach (var item in rbvm.BikesReserved)
                    {
                        using (res = await client.GetAsync($"{baseurl}Bicycles/{item.BycicleId}"))
                        {
                            string apiResponse = await res.Content.ReadAsStringAsync();
                            Bicycle bicycle = JsonConvert.DeserializeObject<Bicycle>(apiResponse);
                            rbvm.Bicycles.Add(bicycle);

                        }
                    }

                    return View(rbvm);
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Reservations/Create
        public async Task<IActionResult> Create()
        {
            
            using (var httpClient = new HttpClient())
            {
                List<Location> locationList = new List<Location>();
                using (var response = await httpClient.GetAsync($"{baseurl}Locations"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    locationList = JsonConvert.DeserializeObject<List<Location>>(apiResponse);
                }           
                ViewData["LocationId"] = new SelectList(locationList, "Id", "City");

                List<ReservationType> reservationList = new List<ReservationType>();           
                using (var response = await httpClient.GetAsync($"{baseurl}ReservationTypes"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<List<ReservationType>>(apiResponse);
                }
            
                ViewData["TypeId"] = new SelectList(reservationList, "Id", "Type");

                List<Customer> customersList = new List<Customer>();           
                using (var response = await httpClient.GetAsync($"{baseurl}Customers"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    customersList = JsonConvert.DeserializeObject<List<Customer>>(apiResponse);
                }
                ViewData["CustomerId"] = new SelectList(customersList, "Id", "LastName");
            }            
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,OutTime,Price,CustomerId,LocationId,TypeId")] Reservation reservation)
        {
            TempData["Message"] = string.Empty;
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri($"{baseurl}Reservations");
                //HTTP POST
                var postTask = httpClient.PostAsJsonAsync<Reservation>("Reservations", reservation);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Reservation has been created.";
                    return RedirectToAction("Index");
                }
            }
            TempData["Message"] = "Reservation has not been created.";
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            List<Location> locationList = new List<Location>();
            using (var httpClient = new HttpClient())
            {
                Reservation reservation = new Reservation();

                using (var response = await httpClient.GetAsync($"{baseurl}Reservations/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservation = JsonConvert.DeserializeObject<Reservation>(apiResponse);
                }

                using (var response = await httpClient.GetAsync($"{baseurl}Locations"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    locationList = JsonConvert.DeserializeObject<List<Location>>(apiResponse);
                }

                List<ReservationType> reservationList = new List<ReservationType>();
                using (var response = await httpClient.GetAsync($"{baseurl}ReservationTypes"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<List<ReservationType>>(apiResponse);
                }

                ViewData["TypeId"] = new SelectList(reservationList, "Id", "Type", reservation.TypeId);

                List<Customer> customersList = new List<Customer>();
                using (var response = await httpClient.GetAsync($"{baseurl}Customers"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    customersList = JsonConvert.DeserializeObject<List<Customer>>(apiResponse);
                }
                ViewData["CustomerId"] = new SelectList(customersList, "Id", "LastName", reservation.CustomerId);            
               
                ViewData["LocationId"] = new SelectList(locationList, "Id", "City", reservation.LocationId);
                return View(reservation);
            }          
            
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OutTime,Price,CustomerId,LocationId,TypeId")] Reservation reservation)
        {

            TempData["Message"] = string.Empty;
            if (reservation.Id > 0 || reservation != null)
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri($"{baseurl}Reservations");
                    HttpResponseMessage res = await httpClient.PutAsJsonAsync($"Reservations/{reservation.Id}", reservation);

                    if (res.IsSuccessStatusCode)
                    {
                        TempData["Message"] = $"Reservation has been saved.";
                        return RedirectToAction("Index");
                    }
                    TempData["Message"] = $"Reservation has not been saved.";
                    return View(reservation);
                }
            }
            List<Location> locationList = new List<Location>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{baseurl}Locations"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    locationList = JsonConvert.DeserializeObject<List<Location>>(apiResponse);
                }
            }
            ViewData["LocationId"] = new SelectList(locationList, "Id", "City", reservation.LocationId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            TempData["Message"] = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                HttpResponseMessage res = await client.DeleteAsync($"Reservations/{id}");
                if (res.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Reservation deleted.";
                    return RedirectToAction("Index");
                }
                TempData["Message"] = "Reservation not deleted.";
                return RedirectToAction("Index");
            }
        }

   
    }
}
