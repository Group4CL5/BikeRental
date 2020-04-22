using System;
using System.Collections.Generic;

namespace BikeRental.Models
{
    public partial class Location
    {
        public Location()
        {
            Employee = new HashSet<Employee>();
            Reservation = new HashSet<Reservation>();
            Return = new HashSet<Return>();
        }

        public int Id { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zipcode { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
        public virtual ICollection<Reservation> Reservation { get; set; }
        public virtual ICollection<Return> Return { get; set; }
    }
}
