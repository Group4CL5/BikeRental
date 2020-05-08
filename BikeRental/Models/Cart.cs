using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeRental.Models
{
    public class Cart
    {
        public List<Bicycle> Bicycles { get; set; } 
        public List<Accessories> Accessories { get; set; }

        public int LocationId { get; set; }
        public DateTime OutTime { get; set; }

        public int TotalCount()
        {
            int b = Bicycles != null ? Bicycles.Count() : 0;
            int a = Accessories != null ? Accessories.Count() : 0;
            return a + b;         
                
        }
        public Cart()
        {
            Bicycles = new List<Bicycle>();
            Accessories = new List<Accessories>();
        }
        public void AddBicycle(Bicycle bicycle)
        {
            Bicycles.Add(bicycle);
        }
        
        public void AddAccessories(Accessories accessories)
        {
            Accessories.Add(accessories);
        }
        
        public void RemoveAccessories(Accessories accessories)
        {
            Accessories.Remove(accessories);
        }

        public void RemoveBicycle(Bicycle bicycle)
        {
            Bicycles.Remove(bicycle);
        }
        public void Checkout()
        {
            Bicycles= new List<Bicycle>();
            Accessories= new List<Accessories>();
        }

    }
}
