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

        [ActionName("CreateNewRentals")]
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

                if (_context.Rentals.Any(r => r.Movie.Id == movie.Id && r.Customer.Id == customer.Id))
                {
                    return BadRequest("This customer already has this movie rented.");
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
        [ActionName("CheckInRental")]
        [HttpPost]
        public IHttpActionResult CheckInRental(NewRentalDto newRentlDto)
        {
            var foundRentalCount = 0;

            var customer = _context.Customers.Single(c => c.Id == newRentlDto.CustomerId);

            var movies = _context.Movies.Where(m => newRentlDto.MovieIds.Contains(m.Id)).ToList();

            foreach (var movie in movies)
            {
                var rental = _context.Rentals.SingleOrDefault(r => r.Customer.Id == customer.Id && r.Movie.Id == movie.Id);

                if (rental != null)
                {
                    foundRentalCount++;
                    _context.Rentals.Remove(rental);
                    movie.NumberAvailable++;
                }
               
            }

            if (foundRentalCount > 0)
            {
                _context.SaveChanges();

                return Ok();
            }

            return NotFound();
        }
    }
}
