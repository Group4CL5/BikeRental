using BikeRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeRental.MVCUI.Models.ViewModels
{
    public class LocationBicycleAccessoriesViewModel
    {
        public Location Location{ get; set; }
        public List<Bicycle> Bicycle { get; set; }
        public List<Accessories> Accessories { get; set; }

        public Cart Cart { get; set; }

    }
}
