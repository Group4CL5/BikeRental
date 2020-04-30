using System;
using System.Collections.Generic;

namespace BikeRental.Models
{
    public partial class Bicycle
    {
        public Bicycle()
        {
            BikeAccessories = new HashSet<BikeAccessories>();
            BikesReserved = new HashSet<BikesReserved>();
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public string Size { get; set; }
        public string Gender { get; set; }
        public string Brand { get; set; }
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }

        public virtual ICollection<BikeAccessories> BikeAccessories { get; set; }
        public virtual ICollection<BikesReserved> BikesReserved { get; set; }
    }
}
