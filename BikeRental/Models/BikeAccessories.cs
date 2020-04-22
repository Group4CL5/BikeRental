using System;
using System.Collections.Generic;

namespace BikeRental.Models
{
    public partial class BikeAccessories
    {
        public int Id { get; set; }
        public int BicycleId { get; set; }
        public int AccessoryId { get; set; }

        public virtual Accessories Accessory { get; set; }
        public virtual Bicycle Bicycle { get; set; }
    }
}
