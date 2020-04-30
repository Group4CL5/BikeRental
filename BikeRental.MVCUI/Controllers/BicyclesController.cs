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

namespace BikeRental.MVCUI.Controllers
{
    public class BicyclesController : Controller
    {
        string baseurl = "https://localhost:44369/api/";       

        // GET: Bicycles
        public async Task<IActionResult> Index()
        {
            List<Bicycle> bicycleList = new List<Bicycle>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{baseurl}Bicycles"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    bicycleList = JsonConvert.DeserializeObject<List<Bicycle>>(apiResponse);
                }
                foreach (var item in bicycleList)
                {
                    using (var response = await httpClient.GetAsync($"{baseurl}Locations/{item.LocationId}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        item.Location = JsonConvert.DeserializeObject<Location>(apiResponse);

                    }
                }
            }
            return View(bicycleList);
        }

        // GET: Bicycles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync($"Bicycles/{id}");
                if (res.IsSuccessStatusCode)
                {
                    var response = res.Content.ReadAsStringAsync().Result;
                    Bicycle bicycle = JsonConvert.DeserializeObject<Bicycle>(response);
                    using (res = await client.GetAsync($"{baseurl}Locations/{bicycle.LocationId}"))
                    {
                        string apiResponse = await res.Content.ReadAsStringAsync();
                        bicycle.Location = JsonConvert.DeserializeObject<Location>(apiResponse);

                    }
                    return View(bicycle);
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Bicycles/Create
        public async Task<IActionResult> Create()
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
            ViewData["LocationId"] = new SelectList(locationList, "Id", "City");
            return View();
        }

        // POST: Bicycles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Type,Size,Gender,Brand,LocationId")] Bicycle bicycle)
        {
            TempData["Message"] = string.Empty;
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri($"{baseurl}Bicycles");
                //HTTP POST
                var postTask = httpClient.PostAsJsonAsync<Bicycle>("Bicycles", bicycle);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Bicycle has been created.";
                    return RedirectToAction("Index");
                }
            }
            TempData["Message"] = "Employee has not been created.";
            return View(bicycle);
        }

        // GET: Bicycles/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            Bicycle bicycle = new Bicycle();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{baseurl}Bicycles/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    bicycle = JsonConvert.DeserializeObject<Bicycle>(apiResponse);
                }
            }
            ViewData["LocationId"] = new SelectList(locationList, "Id", "City", bicycle.LocationId);
            return View(bicycle);
        }

        // POST: Bicycles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Size,Gender,Brand,LocationId")] Bicycle bicycle)
        {
            TempData["Message"] = string.Empty;
            if (bicycle.Id > 0 || bicycle != null)
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri($"{baseurl}Bicycles");
                    HttpResponseMessage res = await httpClient.PutAsJsonAsync($"Bicycles/{bicycle.Id}", bicycle);

                    if (res.IsSuccessStatusCode)
                    {
                        TempData["Message"] = $"Bicycle has been saved.";
                        return RedirectToAction("Index");
                    }
                    TempData["Message"] = $"Bicycle has not been saved.";
                    return View(bicycle);
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
            ViewData["LocationId"] = new SelectList(locationList, "Id", "City", bicycle.LocationId);
            return View(bicycle);
        }

        // GET: Bicycles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            TempData["Message"] = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                HttpResponseMessage res = await client.DeleteAsync($"Bicycles/{id}");
                if (res.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Bicycle deleted.";
                    return RedirectToAction("Index");
                }
                TempData["Message"] = "Bicycle not deleted.";
                return RedirectToAction("Index");
            }
        }

      
      
    }
}
