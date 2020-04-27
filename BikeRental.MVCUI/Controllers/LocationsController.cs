using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BikeRental.Models;
using BikeRental.Controllers;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace BikeRental.MVCUI.Controllers
{
    public class LocationsController : Controller
    {
        string baseurl = "https://localhost:44369/api/";
 
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
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Location location)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri($"{baseurl}Locations");
                    //HTTP POST
                    var postTask = httpClient.PostAsJsonAsync<Location>("Locations", location);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View(location);
        }
        public async Task<IActionResult> Edit(int id)
        {
            Location location = new Location();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{baseurl}Locations/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    location = JsonConvert.DeserializeObject<Location>(apiResponse);
                }
            }
            return View(location);
        }
    }
}
