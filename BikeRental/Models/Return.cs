using System;
using System.Collections.Generic;

namespace BikeRental.Models
{
    public partial class Return
    {
        public int Id { get; set; }
        public DateTime ReturnTime { get; set; }
        public int LocationId { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Location Location { get; set; }
    }
}
