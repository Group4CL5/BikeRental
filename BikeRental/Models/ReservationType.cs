using System;
using System.Collections.Generic;

namespace BikeRental.Models
{
    public partial class ReservationType
    {
        public ReservationType()
        {
            Reservation = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
