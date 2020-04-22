using System;
using System.Collections.Generic;

namespace BikeRental.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Reservation = new HashSet<Reservation>();
            Return = new HashSet<Return>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Reservation> Reservation { get; set; }
        public virtual ICollection<Return> Return { get; set; }
    }
}
