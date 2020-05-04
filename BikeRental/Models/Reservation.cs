using System;
using System.Collections.Generic;

namespace BikeRental.Models
{
    public partial class Reservation
    {
        public Reservation()
        {
            BikesReserved = new HashSet<BikesReserved>();
        }

        public int Id { get; set; }
        public DateTime OutTime { get; set; }
        public decimal Price { get; set; }
        public int CustomerId { get; set; }
        public int LocationId { get; set; }
        public int TypeId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Location Location { get; set; }
        public virtual ReservationType Type { get; set; }
        public virtual ICollection<BikesReserved> BikesReserved { get; set; }
        public virtual ICollection<ReservationAccessories> ReservationAccessories { get; set; }
    }
}
