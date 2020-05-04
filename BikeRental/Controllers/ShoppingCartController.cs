using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BikeRental.Models;

namespace BikeRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly BikeRentalContext _context;

        public ShoppingCartController()
        {
            _context = new BikeRentalContext();
        }
        public void Checkout(Cart cart, int userId)
        {
            Reservation reservation = new Reservation();
            reservation.LocationId = cart.LocationId;
            reservation.OutTime = cart.OutTime;
            reservation.TypeId = 1;
            reservation.Price = 10.00m;
            _context.Reservation.Add(reservation);
            reservation = _context.Reservation.Find(reservation);
            foreach ( var item in cart.Bicycles)
            {
                BikesReserved bikeReserved = new BikesReserved();
                bikeReserved.BycicleId = item.Id;
                bikeReserved.ReservationId = reservation.Id;
                _context.BikesReserved.Add(bikeReserved);
            }
            foreach (var item in cart.Accessories)
            {
                ReservationAccessories reservationAccessories = new ReservationAccessories();
                reservationAccessories.AccessoryId = item.Id;
                reservationAccessories.ReservationId = reservation.Id;
                _context.ReservationAccessories.Add(reservationAccessories);
            }
            cart.Checkout();
        }
        //public void AddBicycles(Cart cart, Bicycle bicycle)
        //{
        //    cart.AddBicycle(bicycle);

        //}
        //public void AddAccessories(Cart cart, Accessories accessories)
        //{
        //    cart.AddAccessories(accessories);
        ////}
        //public void RemoveBicycles(Cart cart, Bicycle bicycle)
        //{
        //    cart.RemoveBicycle(bicycle);

        //}
        //public void RemoveAccessories(Cart cart, Accessories accessories)
        //{
        //    cart.RemoveAccessories(accessories);
        //}
    }
}