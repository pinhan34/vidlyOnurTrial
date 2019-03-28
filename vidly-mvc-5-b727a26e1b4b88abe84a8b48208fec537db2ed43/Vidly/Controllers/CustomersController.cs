using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.MappingViews;
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

        public ViewResult Index()
        {
            var customers = _context.Customers.Include(c => c.MemberShipType).ToList();
            
            return View(customers);
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MemberShipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MemberShipTypes.ToList();

            var customerViewModel = new CustomerViewModel()
            {
                MemberShipTypes = membershipTypes,
            };

            return View("CustomerForm",customerViewModel);
        }

        [HttpPost]
        public ActionResult Save(CustomerViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var viewModelVal = new CustomerViewModel()
                {
                    Customer = viewModel.Customer,
                    MemberShipTypes = _context.MemberShipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }

            if (viewModel.Customer.Id == 0)
            {
                _context.Customers.Add(viewModel.Customer);
            }
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == viewModel.Customer.Id);
                customerInDb.Name = viewModel.Customer.Name;
                customerInDb.Birthdate = viewModel.Customer.Birthdate;
                customerInDb.MemberShipTypeId = viewModel.Customer.MemberShipTypeId;
                customerInDb.IsSubscribedToMyNewsletter = viewModel.Customer.IsSubscribedToMyNewsletter;
            }

            //Mapper.map() => kullanabilirdin...
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerViewModel()
            {
                Customer = customer,
                MemberShipTypes = _context.MemberShipTypes.ToList()
            };

            return View("CustomerForm" , viewModel);
        }

        //protected override void Dispose(bool Disposing)
        //{

        //    if (Disposing)
        //    {
        //        base.Dispose();

        //    }




        //}

        //private IEnumerable<Customer> GetCustomers()
        //{
        //    return new List<Customer>
        //    {
        //        new Customer { Id = 1, Name = "John Smith" },
        //        new Customer { Id = 2, Name = "Mary Williams" }
        //    };
        //}
    }
}