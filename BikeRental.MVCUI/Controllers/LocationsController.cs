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
using System.Web.Mvc;

namespace BikeRental.MVCUI.Controllers
{
    public class LocationsController : Controller
    {
        string baseurl = "https://localhost:44347/api";
        public async Task<ActionResult> IndexAsync()
        {
            IEnumerable<Location> locations = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl); client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("locations");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var locResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    locations = JsonConvert.DeserializeObject<List<Location>>(locResponse);
                }
                return View(locations);

            }
        }
      
    }
}
