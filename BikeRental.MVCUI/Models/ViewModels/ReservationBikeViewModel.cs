using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeRental.Models;

namespace BikeRental.MVCUI.Models.ViewModels
{
    public class ReservationBikeViewModel
    {
        public Reservation Reservation { get; set; }
        public List<BikesReserved> BikesReserved { get; set; } = new List<BikesReserved>();
        public List<Bicycle> Bicycles { get; set; } = new List<Bicycle>();
    }
}
