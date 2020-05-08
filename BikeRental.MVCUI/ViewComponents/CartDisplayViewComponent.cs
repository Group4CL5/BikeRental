using BikeRental.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeRental.MVCUI.Views.ViewComponents
{
    public class CartDisplayViewComponent : ViewComponent
    {
        Cart cart;
        public IViewComponentResult Invoke()
        {
            GetShoppingCart(); 
            return View(cart);
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
    }
}
