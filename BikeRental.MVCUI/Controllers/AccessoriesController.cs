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
    public class AccessoriesController : Controller
    {


        string baseurl = "https://localhost:44369/api/";
        // GET: Accessories
        public async Task<IActionResult> Index()
        {
            List<Accessories> accessoriesList = new List<Accessories>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{baseurl}Accessories"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    accessoriesList = JsonConvert.DeserializeObject<List<Accessories>>(apiResponse);
                }
               
            }
            return View(accessoriesList);
        }

        // GET: Accessories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync($"Accessories/{id}");
                if (res.IsSuccessStatusCode)
                {
                    var response = res.Content.ReadAsStringAsync().Result;
                    Accessories accessories = JsonConvert.DeserializeObject<Accessories>(response);
                    return View(accessories);
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Accessories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Accessories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id, AccessoryName")] Accessories accessories)
        {
            TempData["Message"] = string.Empty;
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri($"{baseurl}Accessories");
                //HTTP POST
                var postTask = httpClient.PostAsJsonAsync<Accessories>("Accessories", accessories);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Accessories has been created.";
                    return RedirectToAction("Index");
                }
            }
            TempData["Message"] = "Accessories has not been created.";
            return View(accessories);
        }

        // GET: Accessories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            
            using (var httpClient = new HttpClient())
            {
                Accessories accessories = new Accessories();

                using (var response = await httpClient.GetAsync($"{baseurl}Accessories/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    accessories = JsonConvert.DeserializeObject<Accessories>(apiResponse);
                }
                return View(accessories);
            }

        }

        // POST: Accessories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, AccessoryName")] Accessories accessories)
        {

            TempData["Message"] = string.Empty;
            if (accessories.Id > 0 || accessories != null)
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri($"{baseurl}Accessories");
                    HttpResponseMessage res = await httpClient.PutAsJsonAsync($"Accessories/{accessories.Id}", accessories);

                    if (res.IsSuccessStatusCode)
                    {
                        TempData["Message"] = $"Accessories has been saved.";
                        return RedirectToAction("Index");
                    }
                    TempData["Message"] = $"Accessories has not been saved.";
                    return View(accessories);
                }
            }
            return View(accessories);
        }

        // GET: Accessories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            TempData["Message"] = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                HttpResponseMessage res = await client.DeleteAsync($"Accessories/{id}");
                if (res.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Accessories deleted.";
                    return RedirectToAction("Index");
                }
                TempData["Message"] = "Accessories not deleted.";
                return RedirectToAction("Index");
            }
        }
    }
}
