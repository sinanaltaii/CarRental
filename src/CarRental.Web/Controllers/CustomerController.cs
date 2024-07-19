using CarRental.Application.Features.Customer.Queries.GetCustomerWithContactInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult CustomerWithContactInfo(CustomerWithContactInfoResponse customerWithContactInfoResponse)
        {
            var routeValues = RouteData;
            var viewData = ViewData;
            var tempData = TempData;
            return View(customerWithContactInfoResponse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchCustomerByPersonalIdentityNumber([FromForm] string personalIdentityNumber)
        {
            var customerWithContactInfo = await _mediator.Send(new GetCustomerWithContactInfoQuery(personalIdentityNumber));
            if (customerWithContactInfo is NullCustomerWithContactInfoResponse)
            {
                ViewData["isSearchPerformed"] = true;
                return View("Index");
            }

            ViewData["customerWithContactInfoResponse"] = customerWithContactInfo;
            RouteData.Values.Add("customerWithContactInfoResponse", customerWithContactInfo);
            return RedirectToAction("CustomerWithContactInfo");
        }
    }
}