using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.api
{
    public class RentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public RentalsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRentlDto)
        {
            var customer = _context.Customers.Single(c => c.Id == newRentlDto.CustomerId);

            var movies = _context.Movies.Where(m => newRentlDto.MovieIds.Contains(m.Id)).ToList();


            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                {
                    return BadRequest("Movie is unavailable.");
                }
                
                movie.NumberAvailable--;

                _context.Rentals.Add(new Rental
                {
                    Customer = customer,
                    Movie = _context.Movies.SingleOrDefault(m => m.Id == movie.Id),
                    DateRented = DateTime.Now
                });
            }

            _context.SaveChanges();

            return Ok();
        }
    }
}
