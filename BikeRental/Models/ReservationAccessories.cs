using System;
using System.Collections.Generic;

namespace BikeRental.Models
{
    public partial class ReservationAccessories
    {
        public int Id { get; set; }
        public int AccessoryId { get; set; }
        public int ReservationId { get; set; }
    }
}
