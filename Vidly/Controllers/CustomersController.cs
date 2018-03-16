using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using System.Web.UI.WebControls;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;
      

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();

            var customerFormViewModel = new CustomerFormViewModel
            {
               
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm",customerFormViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CustomerFormViewModel formViewModel)
        {

            if (!ModelState.IsValid)
            {
                formViewModel.MembershipTypes = _context.MembershipTypes.ToList();
                return View("CustomerForm", formViewModel);
            }

            // new customer
            if (formViewModel.Customer.Id == 0)
            {
                _context.Customers.Add(formViewModel.Customer);
            }
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == formViewModel.Customer.Id);

                customerInDb.BirthDate = formViewModel.Customer.BirthDate;
                customerInDb.IsSubscribedToNewsletter = formViewModel.Customer.IsSubscribedToNewsletter;
                customerInDb.MembershipTypeId = formViewModel.Customer.MembershipTypeId;
                customerInDb.Name = formViewModel.Customer.Name;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }
        // GET: Customers
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }
    }
}