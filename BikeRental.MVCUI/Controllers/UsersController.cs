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
using Microsoft.AspNetCore.Session;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace BikeRental.MVCUI.Controllers
{
    public class UsersController : Controller
    {
        //private readonly BikeRentalContext _context;

        //public UsersController(BikeRentalContext context)
        //{
        //    _context = context;
        //}

        #region viewPages

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
            GetShoppingCart();
            lbavm.Cart = cart;
            return View(lbavm);
        }

        public ActionResult Registration()
        {
            Customer customer = new Customer();
            return View(customer);
        }

        [HttpPost]
        public ActionResult Registration( Customer customer)
        {
            TempData["Message"] = string.Empty;
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri($"{baseurl}Customers");
                //HTTP POST
                var postTask = httpClient.PostAsJsonAsync<Customer>("Customers", customer);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Customer has been created.";
                    return RedirectToAction("Index");
                }
            }
            TempData["Message"] = "Customer has not been created.";
            return View(customer);
        }

        [HttpPost]
        public async Task<ActionResult> Login(Customer customer)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync($"{baseurl}Customers/Email/{customer.Email}");
                if (res.IsSuccessStatusCode)
                {
                    
                    string apiResponse = await res.Content.ReadAsStringAsync();
                    customer = JsonConvert.DeserializeObject<Customer>(apiResponse);
                    HttpContext.Session.SetString("Customer", JsonConvert.SerializeObject(customer));

                    return RedirectToAction("Index");
                  
                }
                return View(customer);
            }
            
        }

        public ActionResult Login()
        {
            Customer customer = new Customer();
            return View();
        }



        #endregion
        #region shoppingCartFunctions

        Cart cart;
        public ActionResult CartDisplay()
        {
            GetShoppingCart();
            LocationBicycleAccessoriesViewModel lbavm = new LocationBicycleAccessoriesViewModel();
            lbavm.Cart = cart;
            return PartialView(lbavm);
        } 

        private void GetShoppingCart()
        {
            var value = HttpContext.Session.GetString("Cart");
            if (value == null) 
            {
                cart = new Cart();
            }
            else
            {
                cart = JsonConvert.DeserializeObject<Cart>(value);
            }
        }



        public ActionResult RemoveBikeFromCart(int id, string url) 
        {
            GetShoppingCart();
            Bicycle bicycle = cart.Bicycles.FirstOrDefault(i => i.Id == id);
            cart.RemoveBicycle(bicycle);
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
            return Redirect(url);
        }
        public ActionResult RemoveAccessoryFromCart(int id, string url) 
        {
            GetShoppingCart();
            Accessories accessories = cart.Accessories.FirstOrDefault(i => i.Id == id);
            cart.RemoveAccessories(accessories);
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
            return Redirect(url);
        }
        public async Task<ActionResult> AddBikeFromCart(int id, string url) 
        {
            GetShoppingCart();
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
                    cart.AddBicycle(bicycle);
                    cart.LocationId = bicycle.LocationId;
                    HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
                    GetShoppingCart();
                    return Redirect(url);
                }
            }
            TempData["Message"] = "Error has occured.";
            return Redirect(url);
        }
        public async Task<ActionResult> AddAccessoryFromCart(int id, string url) 
        {
            GetShoppingCart();
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
                    cart.AddAccessories(accessories);
                    HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
                    return Redirect(url);
                }
            }
            TempData["Message"] = "Error has occured.";
            return Redirect(url);

        }

        public async Task<ActionResult> Checkout()
        {
            GetShoppingCart();
            ReservationType reservationType = new ReservationType();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync($"ReservationTypes");
                if (res.IsSuccessStatusCode)
                {
                    var response = res.Content.ReadAsStringAsync().Result;
                    List<ReservationType> reservationTypes = JsonConvert.DeserializeObject<List<ReservationType>>(response);

                    reservationType = reservationTypes.FirstOrDefault(r => r.Type == "Web");

                }
            }
            var value = HttpContext.Session.GetString("Customer");
            int UserId;
            if (value != null)
            {
                Customer customer = JsonConvert.DeserializeObject<Customer>(value);
                UserId = customer.Id;
            }
            else
            {
                UserId = 1;
            }
            
            Reservation reservation = new Reservation();
            reservation.CustomerId = UserId;
            reservation.LocationId = cart.LocationId;
            reservation.OutTime = DateTime.Now;
            reservation.Price = 10;
            reservation.TypeId = reservationType.Id;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri($"{baseurl}Reservations");
                //HTTP POST
                var postTask = httpClient.PostAsJsonAsync<Reservation>("Reservations", reservation);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    List<Reservation> reservationList = new List<Reservation>();

                    using (var response = await httpClient.GetAsync($"{baseurl}Reservations"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        reservationList = JsonConvert.DeserializeObject<List<Reservation>>(apiResponse);
                        HttpResponseMessage res = await httpClient.GetAsync($"Reservations/{reservationList.Max(r => r.Id)}");
                        if (res.IsSuccessStatusCode)
                        {
                            var resp = res.Content.ReadAsStringAsync().Result;
                            reservation = JsonConvert.DeserializeObject<Reservation>(resp);

                        }
                    }

                }
            }
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri($"{baseurl}BikesReserveds");
                //HTTP POST


                foreach (var item in cart.Bicycles)
                {
                    BikesReserved bikesReserved = new BikesReserved();
                    bikesReserved.ReservationId = reservation.Id;
                    bikesReserved.BycicleId = item.Id;


                    var postTask = httpClient.PostAsJsonAsync<BikesReserved>("BikesReserveds", bikesReserved);
                    postTask.Wait();

                }
            }
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri($"{baseurl}ReservationAccessories");
                //HTTP POST
                
                foreach (var item in cart.Accessories)
                {
                    ReservationAccessories reservationAccessories = new ReservationAccessories();
                    reservationAccessories.ReservationId = reservation.Id;
                    reservationAccessories.AccessoryId = item.Id;
                    

                    var postTask = httpClient.PostAsJsonAsync<ReservationAccessories>("ReservationAccessories", reservationAccessories);
                    postTask.Wait();

                }
            }
            cart.Checkout();
            return View();
   
        }
        #endregion

    
    }
}
