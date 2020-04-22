using System;
using System.Collections.Generic;

namespace BikeRental.Models
{
    public partial class Accessories
    {
        public Accessories()
        {
            BikeAccessories = new HashSet<BikeAccessories>();
        }

        public int Id { get; set; }
        public string AccessoryName { get; set; }

        public virtual ICollection<BikeAccessories> BikeAccessories { get; set; }
    }
}
