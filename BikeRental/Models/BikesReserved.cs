using System;
using System.Collections.Generic;

namespace BikeRental.Models
{
    public partial class BikesReserved
    {
        public int Id { get; set; }
        public int BycicleId { get; set; }
        public int ReservationId { get; set; }

        public virtual Bicycle Bycicle { get; set; }
        public virtual Reservation Reservation { get; set; }
    }
}
